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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data;
    using Gdky.Common;
    using Gdky.Entities;

    namespace Gdky.Application.Interfaces
    {
    /// <summary>
    /// 接口I<xsl:value-of select="NameDx" />
    /// </summary>
    public interface I<xsl:value-of select="NameDx" />Service : IAppService
    {
    #region  成员方法
    /// <summary>
    /// 添加
    /// </summary>
    /// &lt;param name="model"&gt;实体&lt;/param&gt;
    /// <returns></returns>
    ExecutionResult Add(<xsl:value-of select="NameDx" /> model);
    /// <summary>
    /// 修改
    /// </summary>
    /// &lt;param name="model"&gt;实体&lt;/param&gt;
    /// <returns></returns>
    ExecutionResult Update(<xsl:value-of select="NameDx" /> model);
    /// <summary>
    /// 删除
    /// </summary>
    /// &lt;param name="<xsl:value-of select="$IdXx" />"&gt;编号&lt;/param&gt;
    /// <returns></returns>
    ExecutionResult Delete(<xsl:value-of select="$strWhere" />);
    /// <summary>
    /// 批量删除
    /// </summary>
    /// &lt;param name="<xsl:value-of select="$IdXx" />s"&gt;编号&lt;/param&gt;
    /// <returns></returns>
    ExecutionResult DeleteList(string[] <xsl:value-of select="$IdXx" />s);
    /// <summary>
    /// 得到一个对象实体
    /// </summary>
    /// &lt;param name="<xsl:value-of select="$Id" />"&gt;编号&lt;/param&gt;
    /// <returns>实体</returns>；
    <xsl:value-of select="NameDx" /> GetModel(<xsl:value-of select="$strWhere" />);
    /// <summary>
    /// 分页查询数据
    ///</summary>
    /// <param name="page">分页信息</param>
    /// <param name="lambda">查询条件（Expression 表达式树）</param>
    /// <returns></returns>
    PagedData&lt;<xsl:value-of select="NameDx" />&gt; GetPageData(Pagination page, Expression&lt;Func&lt;<xsl:value-of select="NameDx" />, bool&gt;&gt; lambda = null);
    /// <summary>
    /// 获取DataTable
    ///</summary>
    /// <param name="lambda">查询条件（Expression表达式树）</param>
    /// <returns>数据列表</returns>
    DataTable GetDataList(Expression&lt;Func&lt;<xsl:value-of select="NameDx" />, bool&gt;&gt; lambda = null);
    #endregion  成员方法

    #region  扩展方法

    #endregion
    }
    }

  
  </xsl:template>
</xsl:stylesheet>



