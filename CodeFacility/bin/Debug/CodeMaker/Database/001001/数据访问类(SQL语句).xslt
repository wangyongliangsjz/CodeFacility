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
using System;
using System.Collections.Generic;
using System.Data;

namespace Maticsoft.Interface_<xsl:value-of select="Name" />
{
  /// <summary>
  /// <xsl:value-of select="Name" />类
  ///</summary>
  public class <xsl:value-of select="Name" /> : I<xsl:value-of select="Name" />
  {
    private const string Sql_<xsl:value-of select="Name" />_Insert="Insert Into <xsl:value-of select="Name" /> (<xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>
      <xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
    </xsl:for-each><xsl:text>) Values (</xsl:text>
    <xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>@<xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
    </xsl:for-each>
    <xsl:text>)</xsl:text>";
    private const string Sql_<xsl:value-of select="Name" />_Update="Update <xsl:value-of select="Name" /> set <xsl:for-each select="Fields/Field">
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
    private const string Sql_<xsl:value-of select="Name" />_Delete="Delete From <xsl:value-of select="Name" /> Where <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
        <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
        <xsl:if test="ID != $Num"> and </xsl:if>
      </xsl:if>
    </xsl:for-each>";
    private const string Sql_<xsl:value-of select="Name" />_SelectModel="Select <xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>
      <xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
    </xsl:for-each> From <xsl:value-of select="Name" /> Where <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
        <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
        <xsl:if test="ID != $Num"> and </xsl:if>
      </xsl:if>
    </xsl:for-each>";
    private const string Sql_<xsl:value-of select="Name" />_SelectAll="Select <xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>
      <xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
    </xsl:for-each> From <xsl:value-of select="Name" />";
    private const string Sql_<xsl:value-of select="Name" />_SelectPagination="";

        
    #region  成员方法

    /// <summary>
    /// 增加一条数据
    ///</summary>
    public int Add<xsl:value-of select="Name" />(<xsl:value-of select="Name" />Info info)
    {
      int intNum = 0;

      // 定义参数
      ParameterCollection param = new ParameterCollection(<xsl:value-of select="$FieldNum1" />);
      <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='false'">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("@<xsl:value-of select="Name" />", info.<xsl:value-of select="Name" />);
      </xsl:if>
      </xsl:for-each>
      // 执行语句
      using (SqlConnection conn = new SqlConnection(SqlDataAccess.SQLConnString1))
      {
        try
        {
          intNum = SqlDataAccess.ExecuteNonQuery(conn, CommandType.Text, Sql_<xsl:value-of select="$TableName" />_Insert, param);
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
    /// 更新一条数据
    ///</summary>
    public int Edit<xsl:value-of select="Name" />(<xsl:value-of select="Name" />Info info)
    {
      int intNum = 0;
      // 定义参数
      ParameterCollection param = new ParameterCollection(<xsl:value-of select="$FieldNum2" />);
      <xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("@<xsl:value-of select="Name" />", info.<xsl:value-of select="Name" />);
      </xsl:for-each>
      // 执行语句
      using (SqlConnection conn = new SqlConnection(SqlDataAccess.SQLConnString1))
      {
        try
        {
          intNum = SqlDataAccess.ExecuteNonQuery(conn, CommandType.Text, Sql_<xsl:value-of select="$TableName" />_Update, param);
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
    /// 删除一条数据
    ///</summary>
    public int Del<xsl:value-of select="Name" />(<xsl:value-of select="$strWhere" />)
    {
      int intNum = 0;
      // 定义参数
      ParameterCollection param = new ParameterCollection(<xsl:value-of select="$FieldNum3" />);
      <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("@<xsl:value-of select="Name" />", <xsl:value-of select="Name" />);
      </xsl:if>
      </xsl:for-each>
      // 执行语句
      using (SqlConnection conn = new SqlConnection(SqlDataAccess.SQLConnString1))
      {
        try
        {
          intNum = SqlDataAccess.ExecuteNonQuery(conn, CommandType.Text, Sql_<xsl:value-of select="$TableName" />_Delete, param);
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
    /// 得到一个对象实体
    ///</summary>
    public DataSet Get<xsl:value-of select="Name" />OneDataSet(<xsl:value-of select="$strWhere" />)
    {
      // 定义参数
      ParameterCollection param = new ParameterCollection(<xsl:value-of select="$FieldNum3" />);
      <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("@<xsl:value-of select="Name" />", <xsl:value-of select="Name" />);
      </xsl:if>
      </xsl:for-each>
      DataSet ds = new DataSet();
      // 执行语句
      ds = SqlDataAccess.GetDataSet(SqlDataAccess.SQLConnString1, CommandType.Text, Sql_<xsl:value-of select="$TableName" />_SelectModel, param);
      return ds;
    }
    
    /// <summary>
    /// 得到一个对象实体
    ///</summary>
    public <xsl:value-of select="Name" />Info Get<xsl:value-of select="Name" />OneInfo(<xsl:value-of select="$strWhere" />)
    {
      <xsl:value-of select="Name" />Info info = new <xsl:value-of select="Name" />Info();
      // 定义参数
      ParameterCollection param = new ParameterCollection(<xsl:value-of select="$FieldNum3" />);
      <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("@<xsl:value-of select="Name" />", <xsl:value-of select="Name" />);
      </xsl:if>
      </xsl:for-each>

      SqlDataReader dr = null;

      // 执行语句
      using (SqlConnection conn = new SqlConnection(SqlDataAccess.SQLConnString1))
      {
        dr = SqlDataAccess.ExecuteReader(conn, CommandType.Text, Sql_<xsl:value-of select="$TableName" />_SelectModel, param);
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
          </xsl:variable>info.<xsl:value-of select="Name" /> =<xsl:value-of select="$convertBegin" />dr["<xsl:value-of select="Name" />"].ToString()<xsl:value-of select="$convertEnd" />;
        </xsl:for-each>
        }
        else
        {
          return null;
        }
        conn.Close();
      }

      return info;
    }

    /// <summary>
    /// 获得数据列表
    ///</summary>
    public DataSet Get<xsl:value-of select="Name" />AllDataSet()
    {
      // 定义参数
      ParameterCollection param = new ParameterCollection();
      DataSet ds = new DataSet();
      // 执行语句
      ds = SqlDataAccess.GetDataSet(SqlDataAccess.SQLConnString1, CommandType.Text, Sql_<xsl:value-of select="$TableName" />_SelectAll, null);
      return ds;
    }

    /// <summary>
    /// 获得数据列表
    ///</summary>
    public IList&lt;<xsl:value-of select="Name" />Info&gt; Get<xsl:value-of select="Name" />AllList()
    {
      IList&lt;<xsl:value-of select="Name" />Info&gt; ilist=new List&lt;<xsl:value-of select="Name" />Info&gt;();

      // 定义参数
      ParameterCollection param = new ParameterCollection();

      SqlDataReader dr = null;

      // 执行语句
      using (SqlConnection conn = new SqlConnection(SqlDataAccess.SQLConnString1))
      {
        dr = SqlDataAccess.ExecuteReader(conn, CommandType.Text, Sql_<xsl:value-of select="$TableName" />_SelectAll, null);
        while (dr.Read())
        {
          <xsl:value-of select="Name" />Info info = new <xsl:value-of select="Name" />Info();
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
            </xsl:variable>info.<xsl:value-of select="Name" /> =<xsl:value-of select="$convertBegin" />dr["<xsl:value-of select="Name" />"].ToString()<xsl:value-of select="$convertEnd" />;
          </xsl:for-each>
          ilist.Add(info);
        }
        conn.Close();
      }

      return ilist;
    }
    /// <summary>
    /// 根据分页获得数据列表
    ///</summary>
    public DataSet Get<xsl:value-of select="Name" />PageDataSet(int PageSize,int PageIndex,string strWhere)
    {
      // 定义参数
      ParameterCollection param = new ParameterCollection();
      param.Add("@PageSize",PageSize);
      param.Add("@PageIndex",PageIndex);
      DataSet ds = new DataSet();
      // 执行语句
      ds = SqlDataAccess.GetDataSet(SqlDataAccess.SQLConnString1, CommandType.Text, Sql_<xsl:value-of select="$TableName" />_SelectPagination, null);
      return ds;
    }
    #endregion
  }
}
  </xsl:template>
</xsl:stylesheet>


