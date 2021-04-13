<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

  <xsl:output method="xml" indent="yes" />
  <xsl:template match="/*">
    <xsl:for-each select="//Table">
      <xsl:call-template name="table" />
    </xsl:for-each>
  </xsl:template>
  <xsl:variable name="MaxIndex" select="count(/Table/Fields/Field)"/>
  <xsl:template name="table">
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
    <xsl:variable name="strWhere">
      <xsl:for-each select="Fields/Field">
        <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
          <xsl:text disable-output-escaping="yes"></xsl:text>
          <xsl:value-of select="ValueTypeName" />
          <xsl:text> </xsl:text>
          <xsl:value-of select="Name" />
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


//初始化数据
function InitData() {
    $.ajax({ type: "POST",
        contentType: "application/json",
        url: "<xsl:value-of select="Name" />.aspx/GetInit",
        data: "{id:" + $("#keyid").val() + "}",
        dataType: 'json',
        success: function (result, textStatus) {
            $(result.d).each(function () {
                <xsl:for-each select="Fields/Field"><xsl:variable name="I" select="position()"/><xsl:choose><xsl:when test="PrimaryKey = 'false'">$("#txt<xsl:value-of select="NameDx" />").val(this['<xsl:value-of select="Name" />']);
                </xsl:when>
                <xsl:when test="PrimaryKey = 'true'">$("#keyid").val(this['<xsl:value-of select="Name" />']);
                </xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose></xsl:for-each>
            });
        }
    });
}
//清空数据
function EmptyData() {
    <xsl:for-each select="Fields/Field">$("#txt<xsl:value-of select="NameDx" />").val('');
    </xsl:for-each>
}
//添加数据
function AddData() {
    if (validatorForm() == true) {
        $.ajax({ type: "POST",
            contentType: "application/json",
            url: "<xsl:value-of select="Name" />.aspx/Add",
            data: "{<xsl:for-each select="Fields/Field"><xsl:variable name="dyh"><xsl:if test="ValueTypeName = 'string' or ValueTypeName = 'char'">'</xsl:if></xsl:variable><xsl:variable name="I" select="position()"/><xsl:choose><xsl:when test="IsIdentity = 'false' and $I = 2"><xsl:value-of select="Name" />:<xsl:value-of select="$dyh" />" + $("#txt<xsl:value-of select="NameDx" />").val() +
                  </xsl:when>
                  <xsl:when test="IsIdentity = 'false'"><xsl:value-of select="Name" />:<xsl:value-of select="$dyh" />" + $("#txt<xsl:value-of select="NameDx" />").val() +
                  </xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose>"<xsl:value-of select="$dyh" /><xsl:if test="$I != $MaxIndex">,</xsl:if></xsl:for-each>}",
            dataType: 'json',
            success: function (result) {
                JumbotCms.Message(result.d, "1");
                GridPageNav(1);
            }
        });
    }
}
//编辑数据
function EditData() {
    if (validatorForm() == true) {
        $.ajax({ type: "POST",
            contentType: "application/json",
            url: "<xsl:value-of select="Name" />.aspx/Edit",
            data: "{<xsl:for-each select="Fields/Field"><xsl:variable name="dyh"><xsl:if test="ValueTypeName = 'string' or ValueTypeName = 'char'">'</xsl:if></xsl:variable><xsl:variable name="I" select="position()"/>
                  <xsl:if test="PrimaryKey='true'"><xsl:value-of select="Name" />:<xsl:value-of select="$dyh" />" + $("#keyid").val() +
                  </xsl:if>
                  <xsl:if test="PrimaryKey='false'"><xsl:value-of select="Name" />:<xsl:value-of select="$dyh" />" + $("#txt<xsl:value-of select="NameDx" />").val() +
                  </xsl:if>"<xsl:value-of select="$dyh" /><xsl:if test="$I != $MaxIndex">,</xsl:if></xsl:for-each>}",
            dataType: 'json',
            success: function (result) {
                JumbotCms.Message(result.d, "1");
                GridPageNav($('#page').val());
            }
        });
    }
}
//删除数据
function DelData() {
    $.ajax({ type: "POST",
        contentType: "application/json",
        url: "<xsl:value-of select="Name" />.aspx/Delete",
        data: "{id:" + $("#keyid").val() + "}",
        dataType: 'json',
        success: function (result) {
            JumbotCms.Message(result.d, "1");
            GridPageNav(1);
        }
    });
}

  </xsl:template>
</xsl:stylesheet>