<?php
 
$link = pg_connect("host=localhost dbname=test1 user=postgres password=root");
 
if (!$link) {
    echo "DB接続失敗";
}else{
	$questionID = $_POST['questionID'];
	$ansnumber = $_POST['ansnumber'];
	$count =  $_POST['count'];
	#$sql = "SELECT * FROM QUESTIONINFO WHERE questionid ='".$questionID."'";
	#$result = pg_query($link,$sql);
	#$cnt = pg_numrows($result);
	#if($cnt <1){
		echo "新規登録";
		$sql = "INSERT INTO QUESTIONINFO VALUES('".$questionID."',".$count.",".$ansnumber.")";
		$result = pg_query($link,$sql);
	#}else{
	#	echo "更新";;
	#	$sql = "UPDATE QUESTIONINFO SET ansnumber = ".$ansnumber.",count = ".$count." WHERE userid ='".$userID."'";
	#	$result = pg_query($link,$sql);
	#}
}
pg_close($link);
?>
