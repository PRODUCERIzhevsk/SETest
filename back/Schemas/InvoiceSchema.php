<?php

function GetInvoiceXmlSchema()
{
    $xml = '';

    $path = $_SERVER['DOCUMENT_ROOT'].'/Schemas/invoce_schema.xml';
    $handle = @fopen($path, "r");

    if ($handle) {
        while (!feof($handle)) {
            $xml = $xml.fgets($handle, 4096);
        }
        fclose($handle);
    }

    return $xml;
};

function WriteXML($path, $xml)
{
    file_put_contents($path, $xml);
}

?>