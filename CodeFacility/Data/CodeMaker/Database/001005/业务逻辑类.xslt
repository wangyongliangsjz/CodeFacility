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

    #region  成员方法

    /// <summary>
    /// 添加
    ///</summary>
    /// &lt;param name="model"&gt;实体&lt;/param&gt;
    /// <returns></returns>
    public ExecutionResult Add(<xsl:value-of select="NameDx" /> model)
    {
            try
            {
                //model.AddTime = DateTime.Now;
                <xsl:value-of select="NameDx" /> result = this.DbContext.Insert(model);
                if (result != null)
                {
                    return ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt;.Success("添加成功", result);
                }
                return ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt;.Fail("添加失败"); 
            }
            catch(Exception ex)
            {
                LogHelper.WriteWebLog(ex);
                return ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt;.Fail("添加失败"); 
            }
     }

    /// <summary>
    /// 修改
    /// </summary>
    /// &lt;param name="model"&gt;实体&lt;/param&gt;
    /// <returns></returns>
    public ExecutionResult Update(<xsl:value-of select="NameDx" /> model)
    {
        try
        {
            int result = this.DbContext.Update(model);
            if (result > 0)
                return ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt;.Success("编辑成功", model);
            else
                return ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt;.Fail("编辑失败");
        }
        catch (Exception ex)
        {
            LogHelper.WriteWebLog(ex);
            return ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt;.Fail("编辑失败");
        }
    }
    
    /// <summary>
    /// 删除
    /// </summary>
    /// &lt;param name="<xsl:value-of select="$IdXx" />"&gt;编号&lt;/param&gt;
    /// <returns></returns>
    public ExecutionResult Delete(<xsl:value-of select="$strWhere" />)
    {
        try
        {
            int result = this.DbContext.Delete&lt;<xsl:value-of select="NameDx" />&gt;(t => t.<xsl:value-of select="$Id" /> == <xsl:value-of select="$IdXx" />);
            if(result>0)
                return ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt;.Success("删除成功");
            else
                return ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt;.Fail("删除失败");
        }
        catch (Exception ex)
        {
            LogHelper.WriteWebLog(ex);
            return ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt;.Fail("删除失败");
        }
    }
    
    /// <summary>
    /// 批量删除
    /// </summary>
    /// &lt;param name="<xsl:value-of select="$IdXx" />s"&gt;编号&lt;/param&gt;
    /// <returns></returns>
    public ExecutionResult DeleteList(string[] <xsl:value-of select="$IdXx" />s)
   {
        int successNum = 0;
        try
        {
            foreach (string ID in <xsl:value-of select="$IdXx" />s)
            {
                int result = this.DbContext.Delete&lt;<xsl:value-of select="NameDx" />&gt;(t => t.<xsl:value-of select="$Id" /> == ID);
                if (result > 0) successNum++;
            }
            return ExecutionResult.Success("成功删除" + successNum + "条，失败" + (<xsl:value-of select="$IdXx" />s.Length - successNum) + "条");

        }
        catch (Exception ex)
        {
            LogHelper.WriteWebLog(ex);
            return ExecutionResult&lt;<xsl:value-of select="NameDx" />&gt;.Fail("删除失败");
        }
     }

    /// <summary>
    /// 得到一个对象实体
    /// </summary>
    /// &lt;param name="<xsl:value-of select="$IdXx" />"&gt;编号&lt;/param&gt;
    /// <returns>实体</returns>；
    public <xsl:value-of select="NameDx" /> GetModel(<xsl:value-of select="$strWhere" />)
   {
        IQuery&lt;<xsl:value-of select="NameDx" />&gt; query = this.DbContext.Query&lt;<xsl:value-of select="NameDx" />&gt;();
        return query.Where(t => t.<xsl:value-of select="$Id" /> == <xsl:value-of select="$IdXx" />).FirstOrDefault();
    }

    /// <summary>
    /// 分页查询数据
    ///</summary>
    /// <param name="page">分页信息</param>
    /// <param name="lambda">查询条件（Expression 表达式树）</param>
    /// <returns></returns>
    public PagedData&lt;<xsl:value-of select="NameDx" />&gt; GetPageData(Pagination page, Expression&lt;Func&lt;<xsl:value-of select="NameDx" />, bool&gt;&gt; lambda = null)
   {
        IQuery&lt;<xsl:value-of select="NameDx" />&gt; query = this.DbContext.Query&lt;<xsl:value-of select="NameDx" />&gt;();
        if (lambda != null) query = query.Where(lambda);
        PagedData&lt;<xsl:value-of select="NameDx" />&gt; pageData = query.TakePageData(page);
        return pageData;
     }

    /// <summary>
    /// 获取DataTable
    ///</summary>
    /// <param name="lambda">查询条件（Expression表达式树）</param>
    /// <returns>数据列表</returns>
    public DataTable GetDataList(Expression&lt;Func&lt;<xsl:value-of select="NameDx" />, bool&gt;&gt; lambda = null)
    {
        IQuery&lt;<xsl:value-of select="NameDx" />&gt;  query = this.DbContext.Query&lt;<xsl:value-of select="NameDx" />&gt; ();
        if (lambda != null) query = query.Where(lambda);
        List&lt;<xsl:value-of select="NameDx" />&gt;  list = query.OrderByDesc(t => t.<xsl:value-of select="$Id" />).ToList();
        return ListToDataTable(list);
    }

    #endregion

    /// <summary>
    /// 列表转换DataTable
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private DataTable ListToDataTable(List&lt;<xsl:value-of select="NameDx" />&gt;  list)
    {
        DataTable dt = new DataTable();
        DataColumn col = new DataColumn();
        <xsl:for-each select="Fields/Field">col = new DataColumn("<xsl:value-of select="Name" />", typeof(string)); //<xsl:value-of select="Description" />
        dt.Columns.Add(col);</xsl:for-each>
    
        if (list.Count > 0)
        {
            foreach (<xsl:value-of select="NameDx" /> model in list)
            {
                DataRow row = dt.NewRow();
                <xsl:for-each select="Fields/Field">row["<xsl:value-of select="Name" />"] = model.<xsl:value-of select="Name" />;
		        </xsl:for-each>
	            dt.Rows.Add(row);
            }
        }
        else
        {
	            DataRow row = dt.NewRow();
            	<xsl:for-each select="Fields/Field">row["<xsl:value-of select="Name" />"] = "";
		        </xsl:for-each>
	            dt.Rows.Add(row);
        }
        return dt;
    }
    
    #region  扩展方法
    
    #endregion
  }
}
  </xsl:template>
</xsl:stylesheet>


