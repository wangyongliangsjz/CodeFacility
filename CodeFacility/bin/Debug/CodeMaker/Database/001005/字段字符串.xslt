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
  <xsl:variable name="Id">
      <xsl:for-each select="Fields/Field">
        <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
          <xsl:text disable-output-escaping="yes"></xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name" />
          <xsl:if test="ID != $Num"> , </xsl:if>
        </xsl:if>
      </xsl:for-each>
    </xsl:variable>
    <xsl:variable name="IdXx">
      <xsl:for-each select="Fields/Field">
        <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
          <xsl:text disable-output-escaping="yes"></xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="NameXx" />
          <xsl:if test="ID != $Num"> , </xsl:if>
        </xsl:if>
      </xsl:for-each>
    </xsl:variable>
    
--Json字符串
<xsl:text>{</xsl:text>
<xsl:for-each select="Fields/Field">
  <xsl:text disable-output-escaping="yes"></xsl:text>
  <xsl:text>"</xsl:text><xsl:value-of select="Name" /><xsl:text>"</xsl:text><xsl:text>:</xsl:text><xsl:if test="IsString = 'true'">""</xsl:if><xsl:if test="IsString != 'true'">null</xsl:if><xsl:if test="position() != last()">,</xsl:if>
</xsl:for-each><xsl:text>}</xsl:text>

<xsl:text>
</xsl:text>
<xsl:text>{</xsl:text>
<xsl:for-each select="Fields/Field">
  <xsl:text disable-output-escaping="yes"></xsl:text>
  <xsl:text>"</xsl:text><xsl:value-of select="Name" /><xsl:text>"</xsl:text><xsl:text>:</xsl:text><xsl:if test="IsString = 'true'">""</xsl:if><xsl:if test="position() != last()">,</xsl:if>
</xsl:for-each><xsl:text>}</xsl:text>

<xsl:text>
</xsl:text>
<xsl:text>{</xsl:text>
<xsl:for-each select="Fields/Field">
  <xsl:text disable-output-escaping="yes"></xsl:text>
  <xsl:value-of select="Name" /><xsl:text>:</xsl:text><xsl:if test="IsString = 'true'">""</xsl:if><xsl:if test="position() != last()">,</xsl:if>
</xsl:for-each><xsl:text>}</xsl:text>

--Json字符串首字母小写
<xsl:text>{</xsl:text>
<xsl:for-each select="Fields/Field">
  <xsl:text disable-output-escaping="yes"></xsl:text>
  <xsl:text>"</xsl:text><xsl:value-of select="NameXx" /><xsl:text>"</xsl:text><xsl:text>:</xsl:text><xsl:if test="IsString = 'true'">""</xsl:if><xsl:if test="position() != last()">,</xsl:if>
</xsl:for-each><xsl:text>}</xsl:text>

<xsl:text>
</xsl:text>
<xsl:text>{</xsl:text>
<xsl:for-each select="Fields/Field">
  <xsl:text disable-output-escaping="yes"></xsl:text>
  <xsl:value-of select="NameXx" /><xsl:text>:</xsl:text><xsl:if test="IsString = 'true'">""</xsl:if><xsl:if test="position() != last()">,</xsl:if>
</xsl:for-each><xsl:text>}</xsl:text>

--字段类型和字段名称
<xsl:for-each select="Fields/Field">
  <!-- 定义默认值 -->
  <xsl:variable name="defaultvalue">
    <xsl:choose>
      <xsl:when test="ValueTypeName='System.Boolean'">true</xsl:when>
      <xsl:when test="ValueTypeName='System.Byte' or ValueTypeName='System.Int32'">0</xsl:when>
      <xsl:when test="ValueTypeName='System.Single' or ValueTypeName= 'System.Double'">0</xsl:when>
      <xsl:when test="ValueTypeName='System.DateTime'">DateTime.Now</xsl:when>
      <xsl:otherwise>null</xsl:otherwise>
    </xsl:choose>
  </xsl:variable>
<xsl:value-of select="ValueTypeName" /><xsl:if test="Nullable = 'true' and ValueTypeName != 'string' and ValueTypeName != 'bool'">?</xsl:if><xsl:text> </xsl:text><xsl:value-of select="NameXx" /><xsl:text>, </xsl:text>
</xsl:for-each>


--字段说明
<xsl:for-each select="Fields/Field">
  <xsl:text disable-output-escaping="yes"></xsl:text>
  <xsl:value-of select="Description" />:<xsl:value-of select="Name" />
  <xsl:if test="position() != last()">,</xsl:if>
</xsl:for-each>

<xsl:text> 
</xsl:text>

      
  </xsl:template>
</xsl:stylesheet>

