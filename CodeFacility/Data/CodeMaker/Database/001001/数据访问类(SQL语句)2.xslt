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
      <xsl:value-of select="Name" />
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
          <xsl:value-of select="Name" />
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

namespace Maticsoft.Interface_<xsl:value-of select="Name" />
{
  /// <summary>
  /// <xsl:value-of select="Name" />
  ///</summary>
  public class <xsl:value-of select="Name" />
  {   
    #region  成员方法

    /// <summary>
    /// 添加
    ///</summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public int Add(Model.<xsl:value-of select="Name" /> model)
    {
      int intNum = 0;

      // 定义参数
      Hashtable param = new Hashtable();
      <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='false'">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("<xsl:value-of select="Name" />", model.<xsl:value-of select="Name" />);
      </xsl:if>
      </xsl:for-each>
      ParameterCollection pa = SqlDataAccess.Hash2Param(param);
      //Sql语句
      string sql="Insert <xsl:value-of select="Name" /> (<xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>
      <xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
      </xsl:for-each><xsl:text>) Values (</xsl:text>
      <xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>@<xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
      </xsl:for-each>
      <xsl:text>)</xsl:text>";
      // 执行语句
      using (SqlConnection conn = new SqlConnection(SqlDataAccess.SQLConnStringGameGroupBaseDate))
      {
        try
        {
          intNum = SqlDataAccess.ExecuteNonQuery(conn, CommandType.Text, sql, pa);
        }
        catch
        {
          intNum = 0;
        }
        finally
        {
          conn.Close();
        }
      }
      return intNum;
    }

    /// <summary>
    /// 修改
    ///</summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public int Update(Model.<xsl:value-of select="Name" /> model)
    {
      int intNum = 0;
      // 定义参数
      Hashtable param = new Hashtable();
      <xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("<xsl:value-of select="Name" />", model.<xsl:value-of select="Name" />);
      </xsl:for-each>
      ParameterCollection pa = SqlDataAccess.Hash2Param(param);
      //Sql语句
      string sql="Update <xsl:value-of select="Name" /> set <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity!='true' and PrimaryKey!='true'">
        <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
        <xsl:if test="position() != last()">,</xsl:if>
      </xsl:if>
    </xsl:for-each> Where <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
        <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
        <xsl:if test="ID != $Num"> and </xsl:if>
      </xsl:if>
    </xsl:for-each>";
      // 执行语句
      using (SqlConnection conn = new SqlConnection(SqlDataAccess.SQLConnStringGameGroupBaseDate))
      {
        try
        {
          intNum = SqlDataAccess.ExecuteNonQuery(conn, CommandType.Text, sql, pa);
        }
        catch
        {
          intNum = 0;
        }
        finally
        {
          conn.Close();
        }
      }
      return intNum;
    }

    /// <summary>
    /// 删除
    ///</summary>
    ///&lt;param name="<xsl:value-of select="$Id" />"&gt;编号&lt;/param&gt;
    /// <returns></returns>
    public int Delete(<xsl:value-of select="$strWhere" />)
    {
      int intNum = 0;
      // 定义参数
      Hashtable param = new Hashtable();
      <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("<xsl:value-of select="Name" />", <xsl:value-of select="Name" />);
      </xsl:if>
      </xsl:for-each>
      ParameterCollection pa = SqlDataAccess.Hash2Param(param);
      //Sql语句
      string sql="Delete From <xsl:value-of select="Name" /> Where <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
        <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
        <xsl:if test="ID != $Num"> and </xsl:if>
      </xsl:if>
    </xsl:for-each>";
      // 执行语句
      using (SqlConnection conn = new SqlConnection(SqlDataAccess.SQLConnStringGameGroupBaseDate))
      {
        try
        {
          intNum = SqlDataAccess.ExecuteNonQuery(conn, CommandType.Text,  sql, pa);
        }
        catch
        {
          intNum = 0;
        }
        finally
        {
          conn.Close();
        }
      }
      return intNum;
    }

    /// <summary>
    /// 查询一行数据
    ///</summary>
    public DataSet Get<xsl:value-of select="Name" />DataSetOne(<xsl:value-of select="$strWhere" />)
    {
      // 定义参数
      Hashtable param = new Hashtable();
      <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("<xsl:value-of select="Name" />", <xsl:value-of select="Name" />);
      </xsl:if>
      </xsl:for-each>
      ParameterCollection pa = SqlDataAccess.Hash2Param(param);
      //Sql语句
      string sql="Select <xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>
      <xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
    </xsl:for-each> From <xsl:value-of select="Name" /> Where <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
        <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
        <xsl:if test="ID != $Num"> and </xsl:if>
      </xsl:if>
    </xsl:for-each>";

      DataSet ds = new DataSet();
      // 执行语句
      ds = SqlDataAccess.GetDataSet(SqlDataAccess.SQLConnStringGameGroupBaseDate, CommandType.Text, sql, pa);
      return ds;
    }
    <xsl:text>
    </xsl:text>

    public Model.<xsl:value-of select="Name" /> Get<xsl:value-of select="Name" />Model(<xsl:value-of select="$strWhere" />)
    {
      Model.<xsl:value-of select="Name" /> model = new Model.<xsl:value-of select="Name" />();
      // 定义参数
      Hashtable param = new Hashtable();
      <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("<xsl:value-of select="Name" />", <xsl:value-of select="Name" />);
      </xsl:if>
      </xsl:for-each>
      ParameterCollection pa = SqlDataAccess.Hash2Param(param);

      //Sql语句
      string sql="Select <xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>
      <xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
    </xsl:for-each> From <xsl:value-of select="Name" /> Where <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
        <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
        <xsl:if test="ID != $Num"> and </xsl:if>
      </xsl:if>
    </xsl:for-each>";

      SqlDataReader dr = null;

      // 执行语句
      using (SqlConnection conn = new SqlConnection(SqlDataAccess.SQLConnStringGameGroupBaseDate))
      {
        dr = SqlDataAccess.ExecuteReader(conn, CommandType.Text, sql, pa);
        if (dr.Read())
        {
        <xsl:for-each select="Fields/Field">
          <!-- 定义默认值 -->
          <xsl:variable name="convertBegin">
            <xsl:choose>
              <xsl:when test="ValueTypeName='int'">int.Parse(</xsl:when>
              <xsl:when test="ValueTypeName='int16'">Convert.ToInt16(</xsl:when>
              <xsl:when test="ValueTypeName='bool'">Convert.ToBoolean(</xsl:when>
              <xsl:when test="ValueTypeName='DateTime'">Convert.ToDateTime(</xsl:when>
              <xsl:otherwise></xsl:otherwise>
            </xsl:choose>
          </xsl:variable>
          <xsl:variable name="convertEnd">
            <xsl:if test="$convertBegin!=''">)</xsl:if>
          </xsl:variable>model.<xsl:value-of select="Name" /> =<xsl:value-of select="$convertBegin" />dr["<xsl:value-of select="Name" />"].ToString()<xsl:value-of select="$convertEnd" />;
        </xsl:for-each>
        }
        else
        {
          return null;
        }
        conn.Close();
      }

      return model;
    }

    /// <summary>
    /// 查询数据列表
    ///</summary>
    public DataSet Get<xsl:value-of select="Name" />DataSet()
    {
      // 定义参数
      Hashtable param = new Hashtable();
      ParameterCollection pa = SqlDataAccess.Hash2Param(param);
      DataSet ds = new DataSet();
      //Sql语句
      string sql="Select <xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>
      <xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
    </xsl:for-each> From <xsl:value-of select="Name" />";
      // 执行语句
      ds = SqlDataAccess.GetDataSet(SqlDataAccess.SQLConnStringGameGroupBaseDate, CommandType.Text, sql, null);
      return ds;
    }

    /// <summary>
    /// 查询数据列表
    ///</summary>
    public IList&lt;Model.<xsl:value-of select="Name" />&gt; Get<xsl:value-of select="Name" />List()
    {
      IList&lt;Model.<xsl:value-of select="Name" />&gt; ilist=new List&lt;Model.<xsl:value-of select="Name" />&gt;();

      // 定义参数
      Hashtable param = new Hashtable();
      ParameterCollection pa = SqlDataAccess.Hash2Param(param);
      //Sql语句
      string sql="Select <xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>
      <xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
    </xsl:for-each> From <xsl:value-of select="Name" />";
      SqlDataReader dr = null;

      // 执行语句
      using (SqlConnection conn = new SqlConnection(SqlDataAccess.SQLConnStringGameGroupBaseDate))
      {
        dr = SqlDataAccess.ExecuteReader(conn, CommandType.Text, sql,  null);
        while (dr.Read())
        {
          Model.<xsl:value-of select="Name" /> model = new Model.<xsl:value-of select="Name" />();
          <xsl:for-each select="Fields/Field">
            <!-- 定义默认值 -->
            <xsl:variable name="convertBegin">
              <xsl:choose>
                <xsl:when test="ValueTypeName='int'">int.Parse(</xsl:when>
                <xsl:when test="ValueTypeName='int16'">Convert.ToInt16(</xsl:when>
                <xsl:when test="ValueTypeName='bool'">Convert.ToBoolean(</xsl:when>
                <xsl:when test="ValueTypeName='DateTime'">Convert.ToDateTime(</xsl:when>
                <xsl:otherwise></xsl:otherwise>
              </xsl:choose>
            </xsl:variable>
            <xsl:variable name="convertEnd">
              <xsl:if test="$convertBegin!=''">)</xsl:if>
            </xsl:variable>model.<xsl:value-of select="Name" /> =<xsl:value-of select="$convertBegin" />dr["<xsl:value-of select="Name" />"].ToString()<xsl:value-of select="$convertEnd" />;
          </xsl:for-each>
          ilist.Add(model);
        }
        conn.Close();
      }

      return ilist;
    }
    /// <summary>
    /// 根据分页获得数据列表
    ///</summary>
    public DataSet GetListPaginationDataSet(int PageSize,int PageIndex,string strWhere)
    {
      // 定义参数
      Hashtable param = new Hashtable();
      param.Add("PageSize",PageSize);
      param.Add("PageIndex",PageIndex);
      ParameterCollection pa = SqlDataAccess.Hash2Param(param);
      DataSet ds = new DataSet();
      string sql="";
      // 执行语句
      ds = SqlDataAccess.GetDataSet(SqlDataAccess.SQLConnStringGameGroupBaseDate, CommandType.Text, sql, null);
      return ds;
    }
    #endregion
  }
}
  </xsl:template>
</xsl:stylesheet>



