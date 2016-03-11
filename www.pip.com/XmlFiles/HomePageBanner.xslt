<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" version="1.0" encoding="utf-8" indent="yes" omit-xml-declaration="yes"/>

  <xsl:template match="root">
    <div class="main_rotator_wrapper  clearfix">
      <div class="main_rotator">
        <!-- rotator section mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm rotator section -->
        <div id="top_slider">
          <div class="flexslider">
            <ul class="slides">
              <li id="slide_1">
                <div style="width: 1899px; height:500px; float: left; display: block;  url('{backgroundImage/img/@src}')" class="slider-background-image">
                  <xsl:if test="backgroundImage/img != ''">
                    <xsl:attribute name="style">width: 1899px; float: left; display: block;  background-image: url('<xsl:value-of select="backgroundImage/img/@src"></xsl:value-of>')</xsl:attribute>
                  </xsl:if>
                  <div class="slide_container">
                    <div class="flex-image">
                      <a href="{url/a/@href}">
                        <img alt="{image/img/@alt}" src="{image/img/@src}" />
                      </a>
                    </div>
                    <!-- end flex-image -->
                    <div class="flex-caption">
                      <h4>
                        <a style="color:#ffffff;" href="{url/a/@href}">
                          <xsl:value-of select="text"></xsl:value-of>
                        </a>
                      </h4>
                    </div>
                    <!--end class flex-caption-->
                  </div>
                  <!--end class slide_container-->
                </div>
              </li>
            </ul>
           </div>
          <!--end flexslider-->
        </div>
        <!--end #top_slider section-->
        <div class="clear"></div>
      </div>
      <!-- end main_rotator -->
    </div>
  </xsl:template>

</xsl:stylesheet>
