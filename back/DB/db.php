<?php

require_once dirname(__FILE__).'/../Config/DB.php';

function GetUsers()
{
    $users = null;

    $mysqli = new mysqli(DB_HOST, DB_USER, DB_PASSORD, DB_DATABSE_NAME, DB_PORT);
    if ($mysqli->connect_errno) {
        $mysqli->close();
        return $users;
    }

    if (!$mysqli->set_charset("utf8"))
    {
        return $users;
    }

    if ($result = $mysqli->query("SELECT * FROM users_info;")) {
        if ($result->num_rows > 0)
        {
            $users = [];
            while($row = $result->fetch_assoc()) {
                $user = [];
                $user['id'] = $row['id'];
                $user['name'] = $row['name'];
                $user['user_photo_uri'] = $row['user_photo_uri'];
                array_push($users, $user);
            }
        }
        else
        {
            $users = null;
        }

        $result->close();
    }

    $mysqli->close();

    return $users;
}

function GetUserNameById($id)
{
    $name = null;

    $mysqli = new mysqli(DB_HOST, DB_USER, DB_PASSORD, DB_DATABSE_NAME, DB_PORT);
    if ($mysqli->connect_errno) {
        $mysqli->close();
        return $name;
    }

    if (!$mysqli->set_charset("utf8"))
    {
        return $name;
    }

    if ($result = $mysqli->query("call GetUserNameByID({$id})")) {
        if ($result->num_rows > 0)
        {
            $row = $result->fetch_assoc();
            $name = $row['name'];
        }
        else
        {
            $name = null;
        }

        $result->close();
    }

    $mysqli->close();

    return $name;
}

function AddInvoice($IdSender, $IdRecipient, $ItemName)
{
    $mysqli = new mysqli(DB_HOST, DB_USER, DB_PASSORD, DB_DATABSE_NAME, DB_PORT);
    if ($mysqli->connect_errno) {
        $mysqli->close();
        return null;
    }

    if (!$mysqli->set_charset("utf8"))
    {
        return null;
    }

    $xmluri = null;
    if ($mysqli->query("call AddInvoice({$IdSender},{$IdRecipient},\"{$ItemName}\",@id,@xmluri)"))
    {
        if ($resultxml = $mysqli->query("select @xmluri as uri"))
        {
            $row = $resultxml->fetch_assoc();
            $xmluri = $row['uri'];

            $resultxml->close();
        }
    }

    $mysqli->close();
    return $xmluri;
}

function GetInvoices()
{
    $invoices = null;

    $mysqli = new mysqli(DB_HOST, DB_USER, DB_PASSORD, DB_DATABSE_NAME, DB_PORT);
    if ($mysqli->connect_errno) {
        $mysqli->close();
        return $invoices;
    }

    if (!$mysqli->set_charset("utf8"))
    {
        return $invoices;
    }

    if ($result = $mysqli->query("SELECT * FROM invoices;")) {
        if ($result->num_rows > 0)
        {
            $invoices = [];
            while($row = $result->fetch_assoc()) {
                $invoice = [];
                $invoice['id'] = $row['id'];
                $invoice['id_sender'] = $row['id_sender'];
                $invoice['id_recipient'] = $row['id_recipient'];
                $invoice['item_name'] = $row['item_name'];
                $invoice['xml_uri'] = $row['xml_uri'];
                array_push($invoices, $invoice);
            }
        }
        else
        {
            $invoices = null;
        }

        $result->close();
    }

    $mysqli->close();

    return $invoices;
}

?>