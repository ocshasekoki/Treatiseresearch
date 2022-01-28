<?php

$link = pg_connect("host=localhost dbname=test1 user=postgres password=root");
if (!$link) {
    echo "DB接続失敗";
} else {
	$sql = "SELECT * FROM RANK WHERE";
	$fromdate = new DateTime($_GET['fromdate']);
	$todate = new DateTime($_GET['todate']);
	$sql.=" anstime BETWEEN '".date_format($todate,"y-m-d")."' AND '".date_format($fromdate,"y-m-d")."'";
    if(isset($_GET['username']))$sql.=" AND username = '".$_GET['username']."'";
    if($_GET['sortkey']!="percent")$sql.= " ORDER BY ".$_GET['sortkey'];
    else $sql.= " ORDER BY CAST(ansnumber/count AS DECIMAL)";
    if($_GET['sort']=="1")$sql.= " DESC ,ansnumber DESC";
    else $sql.=" ASC ,ansnumber DESC";
	#$sql.=" LIMIT 50";
    $result = pg_query($link, $sql);
    $cnt = pg_numrows($result);
    for ($i = 0; $i < $cnt; $i++) {
    	// 実行結果のi行目の行情報を取り出す
   	 	$r = pg_fetch_row($result, $i);
   	 	$row =['username'=>$r[0],'coin'=>$r[1],'ansnumber'=>$r[2],'anstime'=>$r[3],'count'=>$r[4]];
		$str = json_encode($row,JSON_PRETTY_PRINT);
		echo $str;
    	if($i != $cnt-1)echo '_jnl';
  	}
}
pg_close($link);
?>
