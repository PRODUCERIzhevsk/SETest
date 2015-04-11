<?php
header("Access-Control-Allow-Origin: *");

require_once dirname(__FILE__) . '/../DB/db.php';

$users = GetUsers();

if ($users != null) {
    $json = json_encode($users);
    echo $json;
}
else
{
    echo '';
}

?>