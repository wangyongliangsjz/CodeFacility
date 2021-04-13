<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/*">
    <xsl:if test="name(.) != 'Table' ">
      <font color="red">本模板只能用于单表</font>
      <br />
    </xsl:if>
    <xsl:variable name="classname">
      <xsl:value-of select="concat(Name,'Info')" />
    </xsl:variable>
using System;
using System.Data;
namespace Maticsoft.Model_<xsl:value-of select="Name" />
{
    /// <summary>
    /// <xsl:value-of select="Name" />实体类
    ///</summary>
    [Serializable]
    public class <xsl:value-of select="$classname" />
    {
      ///<summary>
      ///返回数据表名称 <xsl:value-of select="Name" />
      ///</summary>
      public <xsl:value-of select="Name" />Info()
      {
      }
      
      #region 定义数据库字段变量及属性
    <xsl:for-each select="Fields/Field">
      <!-- 定义默认值 -->
      <xsl:variable name="defaultvalue">
        <xsl:choose>
          <xsl:when test="ValueTypeName='System.Boolean'">true</xsl:when>
          <xsl:when test="ValueTypeName='System.Byte' or ValueTypeName='System.Int32'">0</xsl:when>
          <xsl:when test="ValueTypeName='System.Single' or ValueTypeName= 'System.Double'">0</xsl:when>
          <xsl:when test="ValueTypeName='System.DateTime'">DateTime.Now</xsl:when>
          <xsl:otherwise>null</xsl:otherwise>
        </xsl:choose>
      </xsl:variable>
      private <xsl:value-of select="ValueTypeName" /> _<xsl:value-of select="LcName" />;</xsl:for-each>
    
    
    <xsl:for-each select="Fields/Field">
      <xsl:variable name="remark">
        <xsl:if test="string-length( Remark ) > 0 ">
          (<xsl:value-of select="Remark" />)
        </xsl:if>
      </xsl:variable>
      ///<summary>
      /// <xsl:value-of select="Description" />
      ///</summary>
      public <xsl:value-of select="ValueTypeName" /><xsl:text> </xsl:text><xsl:value-of select="Name" />
      {
        get{ return _<xsl:value-of select="LcName" /> ;}
        set{ _<xsl:value-of select="LcName" /> = value;}
      }</xsl:for-each>
      #endregion
    }
}
  </xsl:template>
</xsl:stylesheet>

