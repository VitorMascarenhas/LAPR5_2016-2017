<?php
namespace TukPorto\Utils;

class Utils  {
    static function verificaSessao(){
            if (session_status() == PHP_SESSION_NONE) {
                session_start();
            }
            if (!isset($_SESSION['username'])) {
                return false;
            }
            return true;
    }
    
    static function orderArray($array){
        $aux = array();
        foreach ($array as $key => $row) {
            $aux[$key] = $row['Order'];
        }
        array_multisort($aux, SORT_ASC, $array);
        return $array;
    }
}

