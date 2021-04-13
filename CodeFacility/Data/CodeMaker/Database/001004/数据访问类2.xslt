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
          <xsl:value-of select="Name" />
          <xsl:if test="ID != $Num"> , </xsl:if>
        </xsl:if>
      </xsl:for-each>
    </xsl:variable>
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using Dx.SqlAccess;
using ypp.common;

namespace Maticsoft.Interface_<xsl:value-of select="NameDx" />
{
  /// <summary>
  /// <xsl:value-of select="NameDx" />类
  ///</summary>
  public class <xsl:value-of select="NameDx" /> : I<xsl:value-of select="NameDx" />
  {
    private const string Sql_Insert="Insert Into <xsl:value-of select="Name" /> (<xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity!='true'">
      <xsl:text disable-output-escaping="yes"></xsl:text>
      <xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
      </xsl:if>
    </xsl:for-each><xsl:text>) Values (</xsl:text>
    <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity!='true'">
      <xsl:text disable-output-escaping="yes"></xsl:text>@<xsl:value-of select="Name" />
      <xsl:if test="position() != last()">,</xsl:if>
      </xsl:if>
    </xsl:for-each>
    <xsl:text>)</xsl:text>";
    private const string Sql_Update="Update <xsl:value-of select="Name" /> set <xsl:for-each select="Fields/Field">
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
    private const string Sql_Delete="Delete From <xsl:value-of select="Name" /> Where <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
        <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
        <xsl:if test="ID != $Num"> and </xsl:if>
      </xsl:if>
    </xsl:for-each>";
    private const string Sql_SelectModel="Select * From <xsl:value-of select="Name" /> Where <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
        <xsl:text disable-output-escaping="yes"></xsl:text><xsl:value-of select="Name" /><xsl:text>=</xsl:text>@<xsl:value-of select="Name" />
        <xsl:if test="ID != $Num"> and </xsl:if>
      </xsl:if>
    </xsl:for-each>";
    private const string Sql_SelectPaging="Pro_Get<xsl:value-of select="Name" />Paging";

    private string connString = SqlDataAccess.SQLConnString6;

    #region  成员方法

    /// <summary>
    /// 增加一条数据
    /// </summary>
    public int Add(<xsl:for-each select="Fields/Field"><xsl:variable name="I" select="position()"/><xsl:choose><xsl:when test="IsIdentity = 'false' and $I != $MaxIndex"><xsl:value-of select="ValueTypeName" /><xsl:if test="Nullable = 'true' and ValueTypeName != 'string' and ValueTypeName != 'bool'">?</xsl:if><xsl:text> </xsl:text><xsl:value-of select="Name" />,</xsl:when><xsl:when test="IsIdentity = 'false' and $I = $MaxIndex"><xsl:value-of select="ValueTypeName" /><xsl:if test="Nullable = 'true' and ValueTypeName != 'string' and ValueTypeName != 'bool'">?</xsl:if><xsl:text> </xsl:text><xsl:value-of select="Name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose></xsl:for-each>)
    {
      int intNum = 0;

      // 定义参数
      ParameterCollection param = new ParameterCollection(<xsl:value-of select="$FieldNum1" />);
      <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='false'">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("@<xsl:value-of select="Name" />", <xsl:value-of select="Name" />);
      </xsl:if>
      </xsl:for-each>
      // 执行语句
      using (SqlConnection conn = new SqlConnection(connString))
      {
        try
        {
          intNum = SqlDataAccess.ExecuteNonQuery(conn, CommandType.Text, Sql_Insert, param);
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
    /// </summary>
    public int Edit(<xsl:for-each select="Fields/Field"><xsl:variable name="I" select="position()"/><xsl:choose><xsl:when test="$I != $MaxIndex"><xsl:value-of select="ValueTypeName" /><xsl:if test="Nullable = 'true' and ValueTypeName != 'string' and ValueTypeName != 'bool'">?</xsl:if><xsl:text> </xsl:text><xsl:value-of select="Name" />,</xsl:when><xsl:when test="$I = $MaxIndex"><xsl:value-of select="ValueTypeName" /><xsl:if test="Nullable = 'true' and ValueTypeName != 'string' and ValueTypeName != 'bool'">?</xsl:if><xsl:text> </xsl:text><xsl:value-of select="Name" /></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose></xsl:for-each>)
    {
      int intNum = 0;
      // 定义参数
      ParameterCollection param = new ParameterCollection(<xsl:value-of select="$FieldNum2" />);
      <xsl:for-each select="Fields/Field">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("@<xsl:value-of select="Name" />", <xsl:value-of select="Name" />);
      </xsl:for-each>
      // 执行语句
      using (SqlConnection conn = new SqlConnection(connString))
      {
        try
        {
          intNum = SqlDataAccess.ExecuteNonQuery(conn, CommandType.Text, Sql_Update, param);
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
    /// </summary>
    public bool Delete(<xsl:value-of select="$strWhere" />)
    {
    	int intNum = 0;
        // 定义参数
        ParameterCollection param = new ParameterCollection(<xsl:value-of select="$FieldNum3" />);
        <xsl:for-each select="Fields/Field">
        <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
        <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("@<xsl:value-of select="Name" />", <xsl:value-of select="Name" />);</xsl:if>
        </xsl:for-each>

        // 执行语句
        using (SqlConnection conn = new SqlConnection(connString))
        {
            try
            {
                intNum = SqlDataAccess.ExecuteNonQuery(conn, CommandType.Text, Sql_Delete, param);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
    
    /// <summary>
    /// 得到一个对象实体
    /// </summary>
    public <xsl:value-of select="NameDx" />Info GetInfo(<xsl:value-of select="$strWhere" />)
    {
      <xsl:value-of select="NameDx" />Info info = new <xsl:value-of select="NameDx" />Info();
      // 定义参数
      ParameterCollection param = new ParameterCollection(<xsl:value-of select="$FieldNum3" />);
      <xsl:for-each select="Fields/Field">
      <xsl:if test="IsIdentity='true' or PrimaryKey='true'">
      <xsl:text disable-output-escaping="yes"></xsl:text>param.Add("@<xsl:value-of select="Name" />", <xsl:value-of select="Name" />);
      </xsl:if>
      </xsl:for-each>

      SqlDataReader dr = null;

      // 执行语句
      using (SqlConnection conn = new SqlConnection(connString))
      {
        dr = SqlDataAccess.ExecuteReader(conn, CommandType.Text, Sql_SelectModel, param);
        info = dr.ReaderToModel&lt;<xsl:value-of select="NameDx" />Info&gt;();
        conn.Close();
      }

      return info;
    }

    /// <summary>
    /// 获得数据分页列表
    /// </summary>
    public IList&lt;<xsl:value-of select="NameDx" />Info&gt; GetPagingList(string query, string pageIndex, string pageSize)
    {
      IList&lt;<xsl:value-of select="NameDx" />Info&gt; ilist=new List&lt;<xsl:value-of select="NameDx" />Info&gt;();

      // 定义参数
      ParameterCollection param = new ParameterCollection(3);
      param.Add("@query", query);
	  param.Add("@pageIndex", pageIndex);
      param.Add("@pageSize", pageSize);
      
      SqlDataReader dr = null;

      // 执行语句
      using (SqlConnection conn = new SqlConnection(connString))
      {
        dr = SqlDataAccess.ExecuteReader(conn, CommandType.StoredProcedure, Sql_SelectPaging, param);
        ilist = dr.ReaderToList&lt;<xsl:value-of select="NameDx" />Info&gt;();
        conn.Close();
      }

      return ilist;
    }
    #endregion
  }
}
  </xsl:template>
</xsl:stylesheet>


