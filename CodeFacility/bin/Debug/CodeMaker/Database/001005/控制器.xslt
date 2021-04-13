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
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Data;
using Gdky.Hss.Web.Common;
using Gdky.Application.Interfaces;
using Gdky.Common;
using Gdky.Entities;
using Gdky.Helper;
using Gdky.Hss.Web.Areas.BasicInfo;

namespace Gdky.Hss.Web.Areas.CityHeatAccount
{
  //[LoginAuthorizeAttribute]
  /// <summary>
  /// <xsl:value-of select="NameDx" />类
  ///</summary>
  public class <xsl:value-of select="NameDx" />Controller : WebController
  {

    #region 服务
    private I<xsl:value-of select="NameDx" />Service <xsl:value-of select="NameXx" />Service { get { return this.CreateService&lt;I<xsl:value-of select="NameDx" />Service&gt;(); } }
    #endregion

    #region  视图
    public ActionResult Index()
    {
        return View();
    }
    #endregion

    #region 公有方法

   #region 添加
    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="model">实体</param>
    /// <returns></returns>
    [HttpPost]
    public JsonResult Add<xsl:value-of select="NameDx" />(<xsl:value-of select="NameDx" /> model)
    {
        var result = <xsl:value-of select="NameXx" />Service.Add(model);
        if (!result.IsSuccess)
            return Failed(result.Message);
        return Success("添加成功");
    }
    #endregion

    #region 编辑
    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="model">实体</param>
    /// <returns></returns>
    [HttpPost]
    public JsonResult Update<xsl:value-of select="NameDx" />(<xsl:value-of select="NameDx" /> model)
    {
        var result = <xsl:value-of select="NameXx" />Service.Update(model);
        if (!result.IsSuccess)
            return Failed(result.Message);
        return Success("编辑成功");
    }
    #endregion

    #region 删除
    /// <summary>
    /// 删除
    /// </summary>
    ///&lt;param name="<xsl:value-of select="$IdXx" />"&gt;编号&lt;/param&gt;
    /// <returns></returns>
    [HttpPost]
    public JsonResult Delete<xsl:value-of select="NameDx" />(<xsl:value-of select="$strWhere" />)
    {
        try
        {
            var result = <xsl:value-of select="NameXx" />Service.Delete(<xsl:value-of select="$IdXx" />);
            if (!result.IsSuccess)
                return Failed(result.Message);
            return Success("删除成功");
        }
        catch (Exception ex)
        {
            LogHelper.WriteWebLog(ex);
            return Failed("网络异常");
        }
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// &lt;param name="<xsl:value-of select="$IdXx" />s"&gt;编号&lt;/param&gt;
    /// <returns></returns>
    [HttpPost]
    public JsonResult DeleteList<xsl:value-of select="NameDx" />(string[] <xsl:value-of select="$IdXx" />s)
    {
        var result = <xsl:value-of select="NameXx" />Service.DeleteList(<xsl:value-of select="$IdXx" />s);
        if (!result.IsSuccess)
            return Failed(result.Message);
        return Success("删除成功");
    }
    #endregion

    #region 根据编号获取一条数据

    /// <summary>
    /// 根据编号获取一条数据
    /// </summary>
    /// &lt;param name="<xsl:value-of select="$IdXx" />s"&gt;编号&lt;/param&gt;
    /// <returns></returns>
    public JsonResult GetModelByID<xsl:value-of select="NameDx" />Service(<xsl:value-of select="$strWhere" />)
    {
        <xsl:value-of select="NameDx" /> model = <xsl:value-of select="NameXx" />Service.GetModel(<xsl:value-of select="$IdXx" />);
        if (model == null)
            return Failed("未获取数据");
        else
            return Success(model);
    }

    #endregion

    #region 分页查询

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="page">当前页</param>
    /// <param name="pageSize">页大小</param>
    <xsl:for-each select="Fields/Field">
    /// &lt;param name="<xsl:value-of select="NameXx" />x"&gt;<xsl:value-of select="Description" />&lt;/param&gt;</xsl:for-each>
    /// <returns></returns>
    [HttpPost]
    public JsonResult GetPageData<xsl:value-of select="NameDx" />(int page, int pageSize,<xsl:value-of select="$parameter" />)
    {
        try
        {
            Expression&lt;Func&lt;<xsl:value-of select="NameDx" />, bool&gt;&gt; lambda = t => true;
            <xsl:for-each select="Fields/Field">
            <xsl:variable name="str">
	        <xsl:choose>
	          <xsl:when test="IsString='true'">if (!string.IsNullOrWhiteSpace(<xsl:value-of select="NameXx" />)) lambda = lambda.And(t => t.<xsl:value-of select="Name" />.Contains(<xsl:value-of select="NameXx" />));</xsl:when>
	          <xsl:otherwise>if (<xsl:value-of select="NameXx" /> != null) lambda = lambda.And(t => t.<xsl:value-of select="Name" /> == <xsl:value-of select="NameXx" />);</xsl:otherwise>
	        </xsl:choose>
	      	</xsl:variable>
	      	<xsl:value-of select="$str" />
	      	<xsl:text>
	        </xsl:text>
		    </xsl:for-each>
            Pagination pageInfo = new Pagination();
            pageInfo.Page = page;
            pageInfo.PageSize = pageSize;
            PagedData&lt;<xsl:value-of select="NameDx" />&gt; pagedata = <xsl:value-of select="NameXx" />Service.GetPageData(pageInfo, lambda);
            return Success(pagedata.TotalCount.ToString(), pagedata.DataList);
        }
        catch (Exception ex)
        {
            LogHelper.WriteWebLog(ex);
            return Failed();
        }
    }
    #endregion

    #region 导出列表
    /// <summary>
    /// 导出列表
    /// </summary>
    <xsl:for-each select="Fields/Field">
    /// &lt;param name="<xsl:value-of select="NameXx" />x"&gt;<xsl:value-of select="Description" />&lt;/param&gt;</xsl:for-each>
    /// <returns></returns>
    public void Export<xsl:value-of select="NameDx" />(<xsl:value-of select="$parameter" />)
    {
        try
        {
            Expression&lt;Func&lt;<xsl:value-of select="NameDx" />, bool&gt;&gt; lambda = t => true;
            <xsl:for-each select="Fields/Field">
            <xsl:variable name="str">
	        <xsl:choose>
	          <xsl:when test="IsString='true'">if (!string.IsNullOrWhiteSpace(<xsl:value-of select="NameXx" />)) lambda = lambda.And(t => t.<xsl:value-of select="Name" />.Contains(<xsl:value-of select="NameXx" />));</xsl:when>
	          <xsl:otherwise>if (<xsl:value-of select="NameXx" /> != null) lambda = lambda.And(t => t.<xsl:value-of select="Name" /> == <xsl:value-of select="NameXx" />);</xsl:otherwise>
	        </xsl:choose>
	      	</xsl:variable>
	      	<xsl:value-of select="$str" />
	      	<xsl:text>
	        </xsl:text>
		    </xsl:for-each>
            DataTable dt = <xsl:value-of select="NameXx" />Service.GetDataList(lambda);

            IList&lt;NPOIModel&gt; list = new List&lt;NPOIModel&gt;();
            string title = "文件名" + DateTime.Now.ToString("yyyy-MM-dd");//下载的文件名
            NPOIModel info = new NPOIModel(dt, "<xsl:for-each select="Fields/Field"><xsl:value-of select="Name" />;</xsl:for-each>".ToUpper(),
            title, "<xsl:for-each select="Fields/Field"><xsl:value-of select="Name" />#</xsl:for-each>");
            list.Add(info);
            NPOIHelper.Export(title, list);

        }
        catch (Exception ex)
        {
            LogHelper.WriteWebLog(ex);
        }
    }
    #endregion
    
    #endregion

    #region 私有方法

    #endregion
  }
}
  </xsl:template>
</xsl:stylesheet>


