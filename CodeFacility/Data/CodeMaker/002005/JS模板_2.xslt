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
        tableName: "<xsl:value-of select="Name" />",                      //数据表或视图
        mainKey: "<xsl:for-each select="Fields/Field"><xsl:if test="IsIdentity='true' or PrimaryKey='true'"><xsl:value-of select="Name" /></xsl:if></xsl:for-each>",                             //表主键
        xmlFile: "<xsl:value-of select="Name" />.aspx/GetGrid",
        xslFile: "<xsl:value-of select="Name" />.xslt",                     //表格样式表文件
        tj: "",                                       //查询条件
        isLoad: true,                                 //是否加载数据表格
        pageNav: true,                                //是否带翻页
        procedure: true,                               //是否存储过程
        data: '',                                    //数据  
        dataType: "json"                                    //数据是否是json格式
    }
    GridPageNav(1);
    /*添加/修改游戏测试窗口*/
    $.get("<xsl:value-of select="Name" />.htm?gid=" + Math.random(), function (data) {
        $('#dialog-form').empty().html(data);
        $("#dialog-form").dialog({ autoOpen: false, height: 450, width: 690, modal: true, position: top });
    });
    $.ajax({ type: "GET", url: "/Scripts/validator/easy_validator.pack.js", dataType: "script" });
});

//初始化数据
function InitData() {
    $.ajax({ type: "POST",
        contentType: "application/json",
        url: "<xsl:value-of select="Name" />.aspx/GetInit",
        data: "{id:'" + $("#keyid").val() + "'}",
        dataType: 'json',
        success: function (result, textStatus) {
            $(result.d).each(function () {
                <xsl:for-each select="Fields/Field"><xsl:variable name="I" select="position()"/><xsl:choose><xsl:when test="PrimaryKey = 'false'">$("#txt<xsl:value-of select="Name" />").val(this['<xsl:value-of select="Name" />']);
                </xsl:when>
                <xsl:when test="PrimaryKey = 'true'">$("#keyid").val(this['<xsl:value-of select="Name" />']);
                </xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose></xsl:for-each>
            });
        }
    });
}
//清空数据
function EmptyData() {
    <xsl:for-each select="Fields/Field">$("#txt<xsl:value-of select="Name" />").val('');
    </xsl:for-each>
}
//添加数据
function AddData() {
    if (validatorForm() == true) {
        $.ajax({ type: "POST",
            contentType: "application/json",
            url: "<xsl:value-of select="Name" />.aspx/Add",
            data: "{<xsl:for-each select="Fields/Field"><xsl:variable name="I" select="position()"/><xsl:choose><xsl:when test="IsIdentity = 'false' and $I = 2"><xsl:value-of select="Name" />:'" + $("#txt<xsl:value-of select="Name" />").val() +
                  </xsl:when>
                  <xsl:when test="IsIdentity = 'false'">"',<xsl:value-of select="Name" />:'" + $("#txt<xsl:value-of select="Name" />").val() +
                  </xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose></xsl:for-each>"'}",
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
            data: "{<xsl:for-each select="Fields/Field">
                  <xsl:if test="PrimaryKey='true'"><xsl:value-of select="Name" />:'" + $("#keyid").val() +
                  </xsl:if>
                </xsl:for-each>
                <xsl:for-each select="Fields/Field">
                  <xsl:if test="PrimaryKey='false'">"',<xsl:value-of select="Name" />:'" + $("#txt<xsl:value-of select="Name" />").val() +
                  </xsl:if>
                </xsl:for-each>"'}",
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
        data: "{id:'" + $("#keyid").val() + "'}",
        dataType: 'json',
        success: function (result) {
            JumbotCms.Message(result.d, "1");
            GridPageNav(1);
        }
    });
}
//查询数据
function QueryData() {
    <xsl:for-each select="Fields/Field">var <xsl:value-of select="Name" /> = $("#t<xsl:value-of select="Name" />").val();
    </xsl:for-each>
    
    if (<xsl:for-each select="Fields/Field"><xsl:if test="PrimaryKey='true'"><xsl:value-of select="Name" /> == ""</xsl:if></xsl:for-each>
    <xsl:for-each select="Fields/Field"><xsl:if test="PrimaryKey='false'">&amp;&amp; <xsl:value-of select="Name" /> == ""</xsl:if></xsl:for-each>) {
      JumbotCms.Alert("请输入至少一个以上的查询条件。", "0"); return false;
    }

    <xsl:for-each select="Fields/Field">if (<xsl:value-of select="Name" /> != "") {
      if (_options.tj != "")
      _options.tj = _options.tj + " and <xsl:value-of select="Name" /> like '%" + <xsl:value-of select="Name" /> + "%'";
      else
      _options.tj = " <xsl:value-of select="Name" /> like '%" + <xsl:value-of select="Name" /> + "%'";
    }
    </xsl:for-each>
   
}

//重置按钮
function ReseForm() {
  <xsl:for-each select="Fields/Field">$("#t<xsl:value-of select="Name" />").val('');
  </xsl:for-each>
}

  </xsl:template>
</xsl:stylesheet>
