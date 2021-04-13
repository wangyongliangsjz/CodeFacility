<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

	<xsl:output method="xml" indent="yes" />
	<xsl:template match="/*">
		<xsl:for-each select="//Table">
			<xsl:call-template name="table" />
		</xsl:for-each>
	</xsl:template>
	<xsl:template name="table">
----------- 数据表<xsl:value-of select="Name" />的SQL语句 <xsl:value-of select="count(Fields/Field)" />个字段 --------

<xsl:variable name="Num1">
    <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true'">
        <xsl:value-of select="ID" />
      </xsl:if>
    </xsl:for-each>
  </xsl:variable>
  <xsl:variable name="Num2">
    <xsl:for-each select="Fields/Field">
      <xsl:if test="PrimaryKey='true'">
        <xsl:value-of select="ID" />
      </xsl:if>
    </xsl:for-each>
  </xsl:variable>
  <xsl:variable name="Num">
    <xsl:if test="$Num1!='' and $Num2=''">
      <xsl:value-of select="$Num1" />
    </xsl:if>
    <xsl:if test="$Num1='' and $Num2!=''">
      <xsl:value-of select="$Num2" />
    </xsl:if>
    <xsl:if test="$Num1!='' and $Num2!=''">
      <xsl:if test="$Num1>$Num2">
        <xsl:value-of select="$Num1" />
      </xsl:if>
      <xsl:if test="$Num2>$Num1">
        <xsl:value-of select="$Num2" />
      </xsl:if>
      <xsl:if test="$Num1=$Num2">
        <xsl:value-of select="$Num1" />
      </xsl:if>
    </xsl:if>
  </xsl:variable>
    
--创建表
CREATE TABLE <xsl:value-of select="Name" />( <xsl:for-each select="Fields/Field">
			<xsl:text disable-output-escaping="yes">
     </xsl:text>
			<xsl:value-of select="Name" />
			<xsl:text> </xsl:text>
			<xsl:value-of select="FieldType" />
			<!--<xsl:if test="IsString='true'">(<xsl:value-of select="FieldWidth" />)</xsl:if>-->
			<xsl:if test="Nullable='false'"> NOT NULL</xsl:if>
			<xsl:if test="Nullable='true'"> NULL</xsl:if>
			<xsl:if test="position() != last()"> ,</xsl:if>
		</xsl:for-each> )

--插入记录
Insert into <xsl:value-of select="Name" /> (<xsl:for-each select="Fields/Field">
			<xsl:text disable-output-escaping="yes"></xsl:text> <xsl:value-of select="Name" /><xsl:if test="position() != last()">,</xsl:if>
		</xsl:for-each><xsl:text>)
 Values (</xsl:text>
		<xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>@<xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
		</xsl:for-each>
		<xsl:text>) </xsl:text>

--更新记录
Update <xsl:value-of select="Name" /> set <xsl:for-each select="Fields/Field">
  <xsl:if test="IsIdentity!='true' and PrimaryKey!='true'">
    <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
    <xsl:if test="position() != last()">,</xsl:if>
  </xsl:if>
</xsl:for-each> Where <xsl:for-each select="Fields/Field">
  <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
    <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
    <xsl:if test="ID != $Num"> and </xsl:if>
  </xsl:if>
</xsl:for-each>
GO
--删除记录
Delete From <xsl:value-of select="Name" /> Where <xsl:for-each select="Fields/Field">
  <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
    <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
    <xsl:if test="ID != $Num"> and </xsl:if>
  </xsl:if>
</xsl:for-each>

--查询一条记录
Select <xsl:for-each select="Fields/Field">
  <xsl:text disable-output-escaping="yes"></xsl:text>
  <xsl:value-of select="Name" />
  <xsl:if test="position() != last()">,</xsl:if>
</xsl:for-each> From <xsl:value-of select="Name" /> Where <xsl:for-each select="Fields/Field">
  <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
    <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
    <xsl:if test="ID != $Num"> and </xsl:if>
  </xsl:if>
</xsl:for-each>

--查询表
Select <xsl:for-each select="Fields/Field">
  <xsl:text disable-output-escaping="yes"></xsl:text>
  <xsl:value-of select="Name" />
  <xsl:if test="position() != last()">,</xsl:if>
</xsl:for-each> From <xsl:value-of select="Name" />
<xsl:text> 
</xsl:text>

--字段说明
<xsl:for-each select="Fields/Field">
  <xsl:text disable-output-escaping="yes"></xsl:text>
  <xsl:value-of select="Description" />:<xsl:value-of select="Name" />
  <xsl:if test="position() != last()">,</xsl:if>
</xsl:for-each>

<xsl:text> 
</xsl:text>

--查询表加注释
select <xsl:for-each select="Fields/Field">
			<xsl:text disable-output-escaping="yes">
     </xsl:text>
			<xsl:if test="position() != 1">,</xsl:if><xsl:value-of select="Name" />
			<xsl:text> </xsl:text>--<xsl:text> </xsl:text><xsl:value-of select="Description" /><xsl:text> </xsl:text><xsl:value-of select="FieldType" />
			
		</xsl:for-each> 
from <xsl:value-of select="Name" /> 
      
  </xsl:template>
</xsl:stylesheet>
