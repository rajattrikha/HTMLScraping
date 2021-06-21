<?xml version="1.0" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:template match="/">
        <html>
            <body>
                <table>
                    <tr bgcolor="Gray">
                        <th>Name</th>
                        <th>Model</th>
                        <th>Year</th>
                        <th>Type</th>
                        <th>BodyStyle</th>
                        <th>Price</th>
                        <th>Mileage</th>
                        <th>Interior</th>
                        <th>Exterior</th>
                        <th>Vin</th>
                        <th>Stock</th>
                        <th>Make</th>
                    </tr>
                    <xsl:for-each select="VehicleSearchResult/Vehicles/VehicleDetail">
                        <tr>
                            <td>
                                <xsl:value-of select="Name"/>            
                            </td>
                            <td>
                                <xsl:value-of select="Model"/>            
                            </td>
                            <td>
                                <xsl:value-of select="Year"/>            
                            </td>
                            <td>
                                <xsl:value-of select="Type"/>            
                            </td>
                            <td>
                                <xsl:value-of select="BodyStyle"/>            
                            </td>
                            <td>
                                <xsl:value-of select="Price"/>            
                            </td>
                            <td>
                                <xsl:value-of select="Mileage"/>            
                            </td>
                            <td>
                                <xsl:value-of select="Interior"/>            
                            </td>
                            <td>
                                <xsl:value-of select="Exterior"/>            
                            </td>
                            <td>
                                <xsl:value-of select="Vin"/>            
                            </td>
                            <td>
                                <xsl:value-of select="Stock"/>            
                            </td>
                            <td>
                                <xsl:value-of select="Make"/>            
                            </td>

                        </tr>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>