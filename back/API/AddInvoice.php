<?php
header("Access-Control-Allow-Origin: *");

//http://setest.plexusdev.ru/API/AddInvoice.php?id_sender=2&id_recipient=3&item_name=item&item_count=10&item_price=40
//

if (isset($_REQUEST['id_sender']) &&
    isset($_REQUEST['id_recipient']) &&
    isset($_REQUEST['item_name']) &&
    isset($_REQUEST['item_count']) &&
    isset($_REQUEST['item_price']))
{
    $IdSender = htmlspecialchars($_REQUEST['id_sender']);
    $IdRecipient = htmlspecialchars($_REQUEST['id_recipient']);

    $ItemName = htmlspecialchars($_REQUEST['item_name']);
    $ItemCount = htmlspecialchars($_REQUEST['item_count']);
    $ItemPrice = htmlspecialchars($_REQUEST['item_price']);

    require_once dirname(__FILE__) . '/../DB/db.php';
    $nameSender = GetUserNameById($IdSender);
    $nameRecipient  = GetUserNameById($IdRecipient);

    require_once dirname(__FILE__).'/../Schemas/InvoiceSchema.php';
    $InvoiceXML = GetInvoiceXmlSchema();
    $InvoiceXML = str_replace('#IdSender#', $IdSender, $InvoiceXML);
    $InvoiceXML = str_replace('#NameSender#', $nameSender, $InvoiceXML);
    $InvoiceXML = str_replace('#IdRecipient#', $IdRecipient, $InvoiceXML);
    $InvoiceXML = str_replace('#NameRecipient#', $nameRecipient, $InvoiceXML);
    $InvoiceXML = str_replace('#ItemName#', $ItemName, $InvoiceXML);
    $InvoiceXML = str_replace('#Count#', $ItemCount, $InvoiceXML);
    $InvoiceXML = str_replace('#Price#', $ItemPrice, $InvoiceXML);
    $InvoiceXML = str_replace('#Summ#', (double)$ItemCount * (double)$ItemPrice, $InvoiceXML);

    $xmluri = AddInvoice($IdSender, $IdRecipient, $ItemName);
    if (!is_null($xmluri))
    {
        $xmlPath = $_SERVER['DOCUMENT_ROOT'].'/'.$xmluri;
        WriteXML($xmlPath, $InvoiceXML);
        echo 1;
    }
    else
    {
        echo -2;
    }
}
else
{
    echo -1;
}

?>
