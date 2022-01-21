<?php
 
$link = pg_connect("host=localhost dbname=test1 user=postgres password=root");
 
if (!$link) {
    echo "DB接続失敗";
}else{
	$userID = $_POST['userID'];
	$coin = $_POST['coin'];
	$ansnumber = $_POST['ansnumber'];
	$count =  $_POST['count'];
	$today = date("Y-m-d");
	
	#$sql = "SELECT * FROM RANK WHERE userid ='".$userID."'";
	#$result = pg_query($link,$sql);
	#if(!$result){
		$sql = "INSERT INTO RANK VALUES('".$userID."', '".$coin."', '".$ansnumber."',NOW(),'".$count."')";
		$result = pg_query($link,$sql);
	#}
	$sql = "UPDATE RANK SET coin = '".$coin."', ansnumber = '".$ansnumber."',count = '".$count."'";
	$result = pg_query($link,$sql);
	if (!$result) {
  		echo('データを登録できませんでした。');
	}
}
pg_close($link);
?>
