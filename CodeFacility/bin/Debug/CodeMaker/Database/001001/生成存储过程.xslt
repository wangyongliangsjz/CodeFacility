<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

  <xsl:output method="xml" indent="yes" />
  <xsl:template match="/*">
    <xsl:for-each select="//Table">
      <xsl:call-template name="table" />
    </xsl:for-each>
  </xsl:template>
  <xsl:template name="table">
    ----------- 表<xsl:value-of select="Name" />存储过程 --------

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
    --新增一条记录
    Create Procedure Pro_<xsl:value-of select="Name" />_Add <xsl:for-each select="Fields/Field">
    <xsl:if test="IsIdentity='false'">
    <xsl:text disable-output-escaping="yes"></xsl:text>
    @<xsl:value-of select="Name" /><xsl:text> </xsl:text><xsl:value-of select="FieldType" />
    <xsl:if test="position() != last()">,</xsl:if>
    </xsl:if>
    </xsl:for-each>
    as
    Insert <xsl:value-of select="Name" /> (<xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='false'">
        <xsl:text disable-output-escaping="yes"></xsl:text>
        <xsl:value-of select="Name" />
        <xsl:if test="position() != last()">,</xsl:if>
      </xsl:if>
    </xsl:for-each><xsl:text>)
    Values (</xsl:text>
    <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='false'">
        <xsl:text disable-output-escaping="yes"></xsl:text>@<xsl:value-of select="Name" />
        <xsl:if test="position() != last()">,</xsl:if>
      </xsl:if>
    </xsl:for-each>
    <xsl:text>) </xsl:text>
    GO
    --更新记录
    Create Procedure Pro_<xsl:value-of select="Name" />_Update <xsl:for-each select="Fields/Field">
    <xsl:if test="IsIdentity!='true' and PrimaryKey!='true'">
    <xsl:text disable-output-escaping="yes"></xsl:text>
    @<xsl:value-of select="Name" /><xsl:text> </xsl:text><xsl:value-of select="FieldType" />
    <xsl:if test="position() != last()">,</xsl:if>
    </xsl:if>
    </xsl:for-each>
    as
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
    Create Procedure Pro_<xsl:value-of select="Name" />_Delete
    <xsl:for-each select="Fields/Field">
    <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
    <xsl:text disable-output-escaping="yes"></xsl:text>@<xsl:value-of select="Name" /><xsl:text> </xsl:text><xsl:value-of select="FieldType" />
    <xsl:if test="ID != $Num">,
    </xsl:if>
    </xsl:if>
    </xsl:for-each>
    as
    Delete From <xsl:value-of select="Name" /> Where <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
        <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
        <xsl:if test="ID != $Num"> and </xsl:if>
      </xsl:if>
    </xsl:for-each>
    GO
    --查询一条记录
    Create Procedure Pro_<xsl:value-of select="Name" />_GetModel
    <xsl:for-each select="Fields/Field">
    <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
    <xsl:text disable-output-escaping="yes"></xsl:text>@<xsl:value-of select="Name" /><xsl:text> </xsl:text><xsl:value-of select="FieldType" />
    <xsl:if test="ID != $Num">,
    </xsl:if>
    </xsl:if>
    </xsl:for-each>
    as
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
    GO
    --查询表
    Create Procedure Pro_<xsl:value-of select="Name" />_GetList
    as
    Select <xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>
      <xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
    </xsl:for-each> From <xsl:value-of select="Name" />
    <xsl:text> 
</xsl:text>

  </xsl:template>
</xsl:stylesheet>

