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
	
	$sql = "SELECT * FROM RANK WHERE userid ='".$userID."' AND anstime = date(NOW())";
	$result = pg_query($link,$sql);
	$cnt = pg_numrows($result);
	echo $cnt;
	if($cnt <1){
		echo "新規登録";
		$sql = "INSERT INTO RANK VALUES('".$userID."','".$coin."','".$ansnumber."',NOW(),'".$count."')";
		$result = pg_query($link,$sql);
	}else{
		echo "更新";;
		$sql = "UPDATE RANK SET coin = ".$coin.", ansnumber = ".$ansnumber.",count = ".$count." WHERE userid ='".$userID."'";
		$result = pg_query($link,$sql);
	}
}
pg_close($link);
?>
