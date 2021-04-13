<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/*">
    <xsl:if test="name(.) != 'Table' ">
      <font color="red">本模板只能用于单表</font>
      <br />
    </xsl:if>
    <xsl:variable name="classname">
      <xsl:value-of select="concat(NameDx,'Info')" />
    </xsl:variable>
using System;
using System.Data;
using System.Runtime.Serialization;

namespace Gdky.Application.Models
{
  ///<summary>
  /// <xsl:value-of select="Name" />实体类
  ///</summary>
  public class <xsl:value-of select="NameDx" />
  {
    ///<summary>
    ///返回数据表名称 <xsl:value-of select="Name" />
    ///</summary>
    public <xsl:value-of select="NameDx" />()
    {
    }

    #region
  <xsl:for-each select="Fields/Field">
    <xsl:variable name="remark">
      <xsl:if test="string-length( Remark ) > 0 ">
        (<xsl:value-of select="Remark" />)
      </xsl:if>
    </xsl:variable>
    ///<summary>
    /// <xsl:value-of select="Description" />
    ///</summary>
    public <xsl:value-of select="ValueTypeName" />
    <xsl:if test="Nullable = 'true' and ValueTypeName != 'string' and ValueTypeName != 'bool'">?</xsl:if><xsl:text> </xsl:text><xsl:value-of select="Name" /> { get; set; }</xsl:for-each>
  #endregion
  }
}
  </xsl:template>
</xsl:stylesheet>

