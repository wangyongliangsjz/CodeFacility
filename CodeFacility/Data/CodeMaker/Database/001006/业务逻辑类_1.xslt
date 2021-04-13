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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Gdky.Application.Interfaces;
using Gdky.Entities;
using Gdky.Common;
using Gdky.Helper;
using Chloe;

namespace Gdky.Application.Implements
{
    /// <summary>
    /// <xsl:value-of select="NameDx" />类
    ///</summary>
    public class <xsl:value-of select="NameDx" />Service : AdminAppService,I<xsl:value-of select="NameDx" />Service
    {

    #region  公共方法

    /// <summary>
    /// 添加
    ///</summary>
    /// &lt;param name="model"&gt;实体&lt;/param&gt;
    /// <returns></returns>
    public ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt; Add(<xsl:value-of select="NameDx" /> model)
    {
        model.AddTime = DateTime.Now;
        var result = this.DbContext.Insert(model);
        if (result != null)
        {
            return ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt;.Success("添加成功", result);
        }
        return ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt;.Fail("添加失败"); 
     }

    /// <summary>
    /// 修改
    /// </summary>
    /// &lt;param name="model"&gt;实体&lt;/param&gt;
    /// <returns></returns>
    public ExecutionResult Update(<xsl:value-of select="NameDx" /> model)
    {
        var result = this.DbContext.UpdateOnly(model, t => new
        {<xsl:for-each select="Fields/Field">
			<xsl:text disable-output-escaping="yes">
     		</xsl:text>t.<xsl:value-of select="Name" />,</xsl:for-each> 
        });
        if (result > 0)
            return ExecutionResult.Success("编辑成功");
        else
        return ExecutionResult.Fail("编辑失败");
	}
    
    /// <summary>
    /// 批量删除
    /// </summary>
    /// &lt;param name="IDs"&gt;编号&lt;/param&gt;
    /// <returns></returns>
    public ExecutionResult BatchDelete(string[] IDs)
   {
        int successNum = 0;
        foreach (string ID in IDs)
        {
            int result = this.DbContext.Delete&lt;<xsl:value-of select="NameDx" />&gt;(t => t.<xsl:value-of select="$Id" /> == ID);
            if (result > 0) successNum++;
        }
        return ExecutionResult.Success("成功删除" + successNum + "条，失败" + (IDs.Length - successNum) + "条");
     }

    /// <summary>
    /// 得到一个对象实体
    /// </summary>
    /// &lt;param name="<xsl:value-of select="$Id" />"&gt;编号&lt;/param&gt;
    /// <returns>实体</returns>；
    public <xsl:value-of select="NameDx" /> GetModel(<xsl:value-of select="$strWhere" />)
   {
		var query = GetQuery(t => t.<xsl:value-of select="$Id" /> == <xsl:value-of select="$Id" />);
        return query.FirstOrDefault();
    }

 /// <summary>
    /// 分页查询数据
    ///</summary>
    /// <param name="page">分页信息</param>
    /// <param name="lambda">查询条件（Expression 表达式树）</param>
    /// <returns></returns>
    public PagedData&lt;<xsl:value-of select="NameDx" />&gt; GetPageData(Pagination page, Expression&lt;Func&lt;<xsl:value-of select="NameDx" />, bool&gt;&gt; lambda = null)
   {
        var query = GetQuery(lambda);
        var pageData = query.TakePageData(page);
        return pageData;
     }

    /// <summary>
    /// 获取列表信息
    ///</summary>
    /// <param name="lambda">查询条件（Expression表达式树）</param>
    /// <returns>数据列表</returns>
    public List&lt;<xsl:value-of select="NameDx" />&gt; GetDataList(Expression&lt;Func&lt;<xsl:value-of select="NameDx" />, bool&gt;&gt; lambda = null)
    {
        var query = GetQuery(lambda);
        var list = query.OrderByDesc(t => t.<xsl:value-of select="$Id" />).ToList();
        return list;
    }

    #endregion

    #region  私有方法
    /// <summary>
    /// 获取Query
    ///</summary>
    /// <param name="lambda">查询条件（Expression表达式树）</param>
    /// <returns>数据列表</returns>
    public IQuery&lt;<xsl:value-of select="NameDx" />&gt; GetQuery(Expression&lt;Func&lt;<xsl:value-of select="NameDx" />, bool&gt;&gt; lambda = null)
    {
		var query = this.DbContext.Query&lt;<xsl:value-of select="NameDx" />&gt;().Select(t => new <xsl:value-of select="NameDx" />()
		{
			<xsl:for-each select="Fields/Field">
			<xsl:text disable-output-escaping="yes">
     		</xsl:text>
			<xsl:value-of select="Name" />
			<xsl:text> </xsl:text>=<xsl:text> </xsl:text>t.<xsl:value-of select="Name" /><xsl:if test="position() != last()">,</xsl:if> 
			
		</xsl:for-each> 
		});
        if (lambda != null) query = query.Where(lambda);
        return query;
    }
    #endregion
  }
}
  </xsl:template>
</xsl:stylesheet>


