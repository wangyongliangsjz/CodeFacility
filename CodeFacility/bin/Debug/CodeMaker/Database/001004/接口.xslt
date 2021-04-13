<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

  <xsl:output method="xml" indent="yes" />
  <xsl:template match="/*">
    <xsl:for-each select="//Table">
      <xsl:call-template name="table" />
    </xsl:for-each>
  </xsl:template>
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
          <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="ValueTypeName" />
          <xsl:text> </xsl:text><xsl:value-of select="Name" />
          <xsl:if test="ID != $Num"> , </xsl:if>
        </xsl:if>
      </xsl:for-each>
    </xsl:variable>
    <xsl:variable name="Id">
      <xsl:for-each select="Fields/Field">
        <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
          <xsl:text disable-output-escaping="yes"></xsl:text>
          <xsl:text> </xsl:text><xsl:value-of select="Name" />
          <xsl:if test="ID != $Num"> , </xsl:if>
        </xsl:if>
      </xsl:for-each>
    </xsl:variable>
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Dx.SqlAccess;

namespace Maticsoft.Interface_<xsl:value-of select="NameDx" />
{
  /// <summary>
  /// 接口I<xsl:value-of select="NameDx" />
  /// </summary>
  public interface I<xsl:value-of select="NameDx" />
  {
    #region  成员方法
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    int Add(<xsl:value-of select="NameDx" />Info info);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    int Edit(<xsl:value-of select="NameDx" />Info info);
    /// <summary>
    /// 删除
    /// </summary>
    /// &lt;param name="<xsl:value-of select="$Id" />"&gt;编号&lt;/param&gt;
    /// <returns></returns>
    bool Delete(<xsl:value-of select="$strWhere" />);
    /// <summary>
    /// 得到一个对象实体
    /// </summary>
    /// <returns>实体</returns>；
    <xsl:value-of select="NameDx" />Info GetInfo(<xsl:value-of select="$strWhere" />);
    /// <summary>
    /// 获得数据分页列表
    /// </summary>
    IList&lt;<xsl:value-of select="NameDx" />Info&gt; GetPagingList(string query, string pageIndex, string pageSize);
    #endregion  成员方法
    
    #region  扩展方法
    
    #endregion
   }
}

  </xsl:template>
</xsl:stylesheet>



