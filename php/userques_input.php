<?php
 
$link = pg_connect("host=localhost dbname=test1 user=postgres password=root");
 
if (!$link) {
    echo "DB接続失敗";
}else{
	$userID = $_POST['userID'];
	$ansnumber = $_POST['ansnumber'];
	$count =  $_POST['count'];
	$sql = "SELECT * FROM USERQUES WHERE userid ='".$userID."'";
	$result = pg_query($link,$sql);
	$cnt = pg_numrows($result);
	if($cnt <1){
		echo "新規登録";
		$sql = "INSERT INTO userques VALUES('".$userID."',".$ansnumber.",".$count.")";
		$result = pg_query($link,$sql);
	}else{
		echo "更新";;
		$sql = "UPDATE RANK SET ansnumber = ".$ansnumber.",count = ".$count." WHERE userid ='".$userID."'";
		$result = pg_query($link,$sql);
	}
}
pg_close($link);
?>
