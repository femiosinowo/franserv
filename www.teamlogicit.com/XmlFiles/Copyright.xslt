<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" version="1.0" encoding="utf-8" indent="yes" omit-xml-declaration="yes"/>
  
    <xsl:template match="/">    
      <li>
        <xsl:copy-of select="root/CopyrightText" />
      </li>
      <xsl:for-each select="root/Links">
        <li><xsl:copy-of select="*"/></li>        
      </xsl:for-each>   
  </xsl:template>

</xsl:stylesheet>
