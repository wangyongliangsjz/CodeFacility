<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

  <xsl:output method="xml" indent="yes" />
  <xsl:template match="/*">
    <xsl:for-each select="//Table">
      <xsl:call-template name="table" />
    </xsl:for-each>
  </xsl:template>
  <xsl:template name="table">
    <xsl:variable name="TableName">
      <xsl:value-of select="NameDx" />
    </xsl:variable>
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
    <xsl:variable name="FieldNum1">
      <xsl:if test="$Num1!=''">
        <xsl:value-of select="count(Fields/Field)-1" />
      </xsl:if>
      <xsl:if test="$Num1=''">
        <xsl:value-of select="count(Fields/Field)" />
      </xsl:if>
    </xsl:variable>
    <xsl:variable name="FieldNum2">
      <xsl:value-of select="count(Fields/Field)" />
    </xsl:variable>
    <xsl:variable name="FieldNum3">
      <xsl:if test="$Num1!='' and $Num2=''">1</xsl:if>
      <xsl:if test="$Num1='' and $Num2!=''">1</xsl:if>
      <xsl:if test="$Num1!='' and $Num2!='' and $Num1=$Num1">1</xsl:if>
      <xsl:if test="$Num1!='' and $Num2!=''and $Num1!=$Num1">2</xsl:if>
    </xsl:variable>
    <xsl:variable name="strWhere">
      <xsl:for-each select="Fields/Field">
        <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
          <xsl:text disable-output-escaping="yes"></xsl:text>
          <xsl:value-of select="ValueTypeName" />
          <xsl:text> </xsl:text>
          <xsl:value-of select="NameXx" />
          <xsl:if test="ID != $Num"> , </xsl:if>
        </xsl:if>
      </xsl:for-each>
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
  <xsl:variable name="parameter">
  <xsl:for-each select="Fields/Field">
	<xsl:value-of select="ValueTypeName" /><xsl:if test="Nullable = 'true' and ValueTypeName != 'string' and ValueTypeName != 'bool'">?</xsl:if><xsl:text> </xsl:text><xsl:value-of select="NameXx" /><xsl:text>, </xsl:text>
	</xsl:for-each>
  </xsl:variable>
&lt;?xml version="1.0" encoding="UTF-8"?&gt;
&lt;!DOCTYPE mapper PUBLIC "-//mybatis.org//DTD Mapper 3.0//EN" "http://mybatis.org/dtd/mybatis-3-mapper.dtd"&gt;

&lt;mapper namespace="com.gdky.system.dao.LoginDao"&gt;
	&lt;select id="list" resultType="package"&gt;
		select <xsl:for-each select="Fields/Field">
			<xsl:text disable-output-escaping="yes"></xsl:text>`<xsl:value-of select="Name" />`<xsl:if test="position() != last()">,</xsl:if>
		</xsl:for-each><xsl:text> </xsl:text>from <xsl:value-of select="Name" />
        &lt;where&gt;<xsl:for-each select="Fields/Field">
      &lt;if test="<xsl:value-of select="Name" /> != null and <xsl:value-of select="Name" /> != ''"&gt; and <xsl:value-of select="Name" /> = #{<xsl:value-of select="Name" />} &lt;/if&gt;</xsl:for-each>
    &lt;/where&gt;
	&lt;/select&gt;
	
	&lt;insert id="save" parameterType="package" &gt;
		insert into <xsl:value-of select="Name" />
    (<xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes">
     </xsl:text>`<xsl:value-of select="Name" />`<xsl:if test="position() != $FieldNum2">,</xsl:if>
    </xsl:for-each>
    )
    values
    (<xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes">
     </xsl:text>#{<xsl:value-of select="Name" />}<xsl:if test="position() != $FieldNum2">,</xsl:if>
    </xsl:for-each>
		)
	&lt;/insert&gt;
	
	&lt;update id="update" parameterType="package"&gt;
		update <xsl:value-of select="Name" /> 
		&lt;set&gt;
    <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity!='true' and PrimaryKey!='true'">&lt;if test="<xsl:value-of select="Name" /> != null"&gt;`<xsl:value-of select="Name" />` = #{<xsl:value-of select="Name" />}, &lt;/if&gt;
      </xsl:if>
    </xsl:for-each>
		&lt;/set&gt;
		where <xsl:for-each select="Fields/Field">
  <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
    <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>#{<xsl:value-of select="Name" />}
    <xsl:if test="ID != $Num"> and </xsl:if>
  </xsl:if>
    </xsl:for-each>&lt;/update&gt;
	
	&lt;delete id="remove"&gt;
		delete from <xsl:value-of select="Name" /> where <xsl:for-each select="Fields/Field">
    <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
      <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>#{value}
      <xsl:if test="ID != $Num"> and </xsl:if>
    </xsl:if>
  </xsl:for-each>&lt;/delete&gt;
&lt;/mapper&gt;

  </xsl:template>
</xsl:stylesheet>