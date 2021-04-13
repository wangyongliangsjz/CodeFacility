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
    
var sXMLFile = "/Counter/XmlFile/XmlDdlData.aspx?pId=";
var sXSLTFile = "/Counter/XslFile/XslDdlData.xslt";

$(function () {
    //加载数据表
    _options = {
        db: "p2p",
        tableName: "<xsl:value-of select="Name" />",                      //数据表或视图
        mainKey: "<xsl:for-each select="Fields/Field"><xsl:if test="IsIdentity='true' or PrimaryKey='true'"><xsl:value-of select="Name" /></xsl:if></xsl:for-each>",                             //表主键
        xslFile: "<xsl:value-of select="Name" />.xslt",                     //表格样式表文件
        tj: "",                                       //查询条件
        isLoad: true,                                 //是否加载数据表格
        pageNav: true,                                //是否带翻页
        procedure: false
    }
    GridPageNav(1);
    /*添加/修改窗口*/
    $.get("<xsl:value-of select="Name" />.htm?gid=" + Math.random(), function (data) {
        $('#dialog-form').empty().html(data);
        $("#dialog-form").dialog({ autoOpen: false, height: 390, width: 550, modal: true, position: top });
    });
    $.ajax({ type: "GET", url: "/Scripts/validator/easy_validator.pack.js", dataType: "script" });
})



//查询数据
function QueryData() {
    <xsl:for-each select="Fields/Field">var <xsl:value-of select="Name" /> = $("#t<xsl:value-of select="NameDx" />").val();
    </xsl:for-each>
    var startTime = $("#tStartTime").val();
    var endTime = $("#tEndTime").val();
    
    if (<xsl:for-each select="Fields/Field"><xsl:variable name="I" select="position()"/><xsl:value-of select="Name" /> == ""<xsl:if test=" $I != $MaxIndex"> &amp;&amp; </xsl:if></xsl:for-each> &amp;&amp; (startTime == "" || endTime == "")) {
      JumbotCms.Alert("请输入至少一个以上的查询条件。", "0"); return false;
    }
    
    <xsl:for-each select="Fields/Field"><xsl:variable name="tjfh"><xsl:choose><xsl:when test="ValueTypeName = 'string' or ValueTypeName = 'char'"><xsl:text> CHARINDEX('</xsl:text></xsl:when><xsl:otherwise><xsl:value-of select="Name" /><xsl:text> =</xsl:text></xsl:otherwise></xsl:choose></xsl:variable>if (<xsl:value-of select="Name" /> != "") {
      if (_options.tj != "")
      _options.tj = _options.tj + " and <xsl:value-of select="$tjfh" />" + <xsl:value-of select="Name" /><xsl:if test="ValueTypeName = 'string' or ValueTypeName = 'char'"><xsl:text> + "',</xsl:text><xsl:value-of select="Name" /><xsl:text>) > 0"</xsl:text></xsl:if>;
      else
      _options.tj = " <xsl:value-of select="$tjfh" />" + <xsl:value-of select="Name" /><xsl:if test="ValueTypeName = 'string' or ValueTypeName = 'char'"><xsl:text> + "',</xsl:text><xsl:value-of select="Name" /><xsl:text>) > 0"</xsl:text></xsl:if>;
    }
    </xsl:for-each>
    if (startTime != "" &amp;&amp; endTime != "") {
        if (_options.tj != "")
            _options.tj = _options.tj + " and Time between '" + startTime + " 00:00:00' and '" + endTime + " 23:59:59'";
        else
            _options.tj = " Time between '" + startTime + " 00:00:00' and '" + endTime + " 23:59:59'";
    }
}

//重置按钮
function ReseForm() {
  <xsl:for-each select="Fields/Field">$("#t<xsl:value-of select="NameDx" />").val('');
  </xsl:for-each>
  $("#tStartTime").val('');
  $("#tEndTime").val('');
}

  </xsl:template>
</xsl:stylesheet>