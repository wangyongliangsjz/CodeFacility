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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

namespace WebUI.billflow.gametest
{

  public partial class <xsl:value-of select="Name" /> : PageBase
  {
      private static I<xsl:value-of select="NameDx" /> dal = new <xsl:value-of select="NameDx" />();

      protected void Page_Load(object sender, EventArgs e)
      {
          PageBegin("");
      }

      [WebMethod]
      public static <xsl:value-of select="NameDx" />Info GetInit(int id)
      {
          return dal.GetInfo(id);
      }

      [WebMethod]
      public static string Add(<xsl:for-each select="Fields/Field"><xsl:variable name="I" select="position()"/><xsl:choose><xsl:when test="IsIdentity = 'false' and $I != $MaxIndex"><xsl:value-of select="ValueTypeName" /><xsl:if test="Nullable = 'true' and ValueTypeName != 'string' and ValueTypeName != 'bool'">?</xsl:if><xsl:text> </xsl:text><xsl:value-of select="Name" />,</xsl:when><xsl:when test="IsIdentity = 'false' and $I = $MaxIndex"><xsl:value-of select="ValueTypeName" /><xsl:if test="Nullable = 'true' and ValueTypeName != 'string' and ValueTypeName != 'bool'">?</xsl:if><xsl:text> </xsl:text><xsl:value-of select="Name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose></xsl:for-each>)
      {
          if (dal.Add(<xsl:for-each select="Fields/Field"><xsl:variable name="I" select="position()"/><xsl:if test="IsIdentity = 'false'"><xsl:value-of select="Name" /></xsl:if><xsl:if test="IsIdentity = 'false' and $I != $MaxIndex">,</xsl:if></xsl:for-each>) > 0)
              return "添加数据成功。";
          else
              return "添加数据失败！";
      }

      [WebMethod]
      public static string Edit(<xsl:for-each select="Fields/Field"><xsl:variable name="I" select="position()"/><xsl:choose><xsl:when test="$I != $MaxIndex"><xsl:value-of select="ValueTypeName" /><xsl:if test="Nullable = 'true' and ValueTypeName != 'string' and ValueTypeName != 'bool'">?</xsl:if><xsl:text> </xsl:text><xsl:value-of select="Name" />,</xsl:when><xsl:when test="$I = $MaxIndex"><xsl:value-of select="ValueTypeName" /><xsl:if test="Nullable = 'true' and ValueTypeName != 'string' and ValueTypeName != 'bool'">?</xsl:if><xsl:text> </xsl:text><xsl:value-of select="Name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose></xsl:for-each>)
      {
          if (dal.Edit(<xsl:for-each select="Fields/Field"><xsl:variable name="I" select="position()"/><xsl:value-of select="Name" /><xsl:if test="$I != $MaxIndex">,</xsl:if></xsl:for-each>) > 0)
              return "编辑数据成功。";
          else
              return "编辑数据失败！";
      }

      [WebMethod]
      public static string Delete(int id)
      {
          if (dal.Delete(id))
              return "删除数据成功。";
          else
              return "删除数据失败！";
      }
      
      [WebMethod]
      public static string GetGrid(string query, string pageIndex, string pageSize)
      {
          Dx.Purview.IOperator opdal = new Dx.Purview.Operator();
          string operatorId = opdal.GetCRMPurviewByOperatorId(HttpContext.Current.Session["UserId"].ToString());
          DataSet ds = dal.GetGrid(operatorId, query, pageIndex, pageSize);
          ds.DataSetName = "NewDataSet";
          ds.Tables[0].TableName = "V_<xsl:value-of select="Name" />";
          ds.Tables[1].TableName = "PageNav";
          return ds.GetXml();
      }


  }
}
  </xsl:template>
</xsl:stylesheet>