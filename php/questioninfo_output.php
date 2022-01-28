<?php
 
$link = pg_connect("host=localhost dbname=test1 user=postgres password=root");
 
if (!$link) {
    echo "DB接続失敗";
} else {
    $sql = "SELECT * FROM QUESTIONINFO";
    
    $result = pg_query($link, $sql);
    $cnt = pg_numrows($result);
    for ($i = 0; $i < $cnt; $i++) {
    	// 実行結果のi行目の行情報を取り出す
   	 	$r = pg_fetch_row($result, $i);
   	 	$row =['questionid'=>$r[0],'count'=>$r[1],'ansnumber'=>$r[2]];
		$str = json_encode($row,JSON_PRETTY_PRINT);
		echo $str;
    	if($i != $cnt-1)echo '_jnl';
  	}
}
pg_close($link);
?>
