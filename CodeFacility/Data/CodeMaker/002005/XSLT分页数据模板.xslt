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
          <xsl:value-of select="Name" />
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
    <xsl:variable name="I">
      <xsl:text disable-output-escaping="yes">{$I}</xsl:text>
    </xsl:variable>
    <xsl:variable name="id">
      <xsl:text disable-output-escaping="yes">{$id}</xsl:text>
    </xsl:variable>&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"&gt;
  &lt;xsl:template match="NewDataSet"&gt;  
    <table id="Grid" cellspacing="0" border="1">
      <xsl:text disable-output-escaping="yes">
      </xsl:text>
      <thead onclick="sortColumn(event)">
        <xsl:text disable-output-escaping="yes">
        </xsl:text>
        <tr class="tabletitle">
          <xsl:text disable-output-escaping="yes">
          </xsl:text>
          <td>X</td>
          <xsl:text disable-output-escaping="yes">
          </xsl:text>
          <xsl:for-each select="Fields/Field">
              <xsl:text disable-output-escaping="yes"></xsl:text>&lt;th style="width: 10%"&gt;<xsl:value-of select="Description" />&lt;/th&gt;
         </xsl:for-each>
          <th style="width: 10%">操作</th>
          <xsl:text disable-output-escaping="yes">
          </xsl:text>
        </tr>
        <xsl:text disable-output-escaping="yes">
        </xsl:text>
      </thead>
      <xsl:text disable-output-escaping="yes">
      </xsl:text>
      <tbody>
        &lt;xsl:for-each select="<xsl:value-of select="Name" />"&gt;
        &lt;xsl:variable name="I" select="position()"/&gt;
        &lt;xsl:variable name="id"&gt;
        &lt;xsl:value-of select="<xsl:for-each select="Fields/Field"><xsl:if test="PrimaryKey='true'"><xsl:value-of select="Name" /></xsl:if></xsl:for-each>" /&gt;
          &lt;/xsl:variable&gt;
          <tr onclick="subexpandIt('{$I}','{$id}');" ondblclick="btnEdit_click();" title="双击进行编辑" onmouseover="Mover(this)" onmouseout="Mout(this)">
            <xsl:text>
              </xsl:text>
            <td class="GridLeftIco" id="ico{$I}">4</td>
            <xsl:for-each select="Fields/Field">
              <xsl:text disable-output-escaping="yes">
              </xsl:text>
              <td>
                &lt;xsl:value-of select="<xsl:value-of select="Name" />"/&gt;
              </td>
            </xsl:for-each>
            <xsl:text disable-output-escaping="yes">
            </xsl:text>
              <td>
              <a href="javascript:void(0)" onclick="ViewQueryData('{$id}')">
                <img src="/Images/fileIco/defaut.gif" title="查看" style="height:15px; width:15px"></img>
              </a>
              &lt;xsl:text disable-output-escaping="yes"&gt;&#160;&lt;/xsl:text&gt;
              <a href="javascript:void(0)" onclick="ViewEditData('{$id}')">
                <img src="/Images/stock/edit.gif" title="编辑"></img>
              </a>
              &lt;xsl:text disable-output-escaping="yes"&gt;&#160;&lt;/xsl:text&gt;
              <a href="javascript:void(0)" onclick="ViewDelData('{$id}')">
                <img src="/Images/stock/del.gif" title="删除"></img>
              </a>
                <xsl:text disable-output-escaping="yes">
            </xsl:text>
            </td>
            <xsl:text disable-output-escaping="yes">
            </xsl:text>
          </tr>
        &lt;/xsl:for-each&gt;
      </tbody>
    </table>
  &lt;/xsl:template&gt;
&lt;/xsl:stylesheet&gt;


  </xsl:template>
</xsl:stylesheet>