<?php
header("Access-Control-Allow-Origin: *");

require_once dirname(__FILE__) . '/../DB/db.php';

$invoices = GetInvoices();

if ($invoices != null) {
    $json = json_encode($invoices);
    echo $json;
}
else
{
    echo '';
}

?>