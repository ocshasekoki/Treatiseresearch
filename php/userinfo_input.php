<?php
 
$link = pg_connect("host=localhost dbname=test1 user=postgres password=root");
 
if (!$link) {
    echo "DB接続失敗";
}else{
	$userID = $_POST['userID'];
	$username = $_POST['username'];
	$email =  $_POST['email'];
	$email =  $_POST['password'];
	$sql = "SELECT * FROM USERQUES WHERE userid ='".$userID."'";
	$result = pg_query($link,$sql);
	$cnt = pg_numrows($result);
	if($cnt <1){
		echo "新規登録";
		$sql = "INSERT INTO userques VALUES('".$userID."','".$username."','".$email."','".$password."')";
		$result = pg_query($link,$sql);
	}else{
		echo "登録済";
	}
}
pg_close($link);
?>
