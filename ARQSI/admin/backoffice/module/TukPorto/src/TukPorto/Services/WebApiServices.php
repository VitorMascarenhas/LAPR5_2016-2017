<?php
namespace TukPorto\Services;

use Zend\Http\Client;
use Zend\Http\Request;
use Zend\Json\Json;

// Carrega e envia dados diretamente da API

class WebApiServices {
    
    public static $enderecoBase ='http://10.8.11.147';


    # Carrega estatisticas - GET
    public static function getStats() {
        $client = new Client(WebApiServices::$enderecoBase . '/api/stats');
        $client->setMethod(Request::METHOD_GET);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $response = $client->send();
        $body = $response->getBody();
        $data = Json::decode($response->getBody(), true);
        return $data;
    }

    # Carrega users - GET
    public static function getUsers() {
        $client = new Client(WebApiServices::$enderecoBase . '/api/Account/users');
        $client->setMethod(Request::METHOD_GET);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $response = $client->send();
        $body = $response->getBody();
        $data = Json::decode($response->getBody(), true);
        return $data;
    }
    

    # Carrega Localizações - GET
    public static function getLocations() {
        $client = new Client(WebApiServices::$enderecoBase . '/api/location');
        $client->setMethod(Request::METHOD_GET);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $response = $client->send();
        $body = $response->getBody();
        $data = Json::decode($response->getBody(), true);
        return $data;
    }
    
    # Carrega Percursos - GET
    public static function getPercursos() {
        $client = new Client(WebApiServices::$enderecoBase . '/api/Visit');
        $client->setMethod(Request::METHOD_GET);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $response = $client->send();
        $body = $response->getBody();
        $data = Json::decode($response->getBody(), true);
        return $data;
    }
    

    # Carrega um Poi por id - GET
    public static function getPercurso($id){
        $client = new Client(WebApiServices::$enderecoBase . '/api/Visit/' . $id);
        $client->setMethod(Request::METHOD_GET);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $response = $client->send();
        $body = $response->getBody();
        $data = Json::decode($response->getBody(), true);
        return $data;
    }
    

    # Carrega Pois - GET
    public static function getPois() {
        $client = new Client(WebApiServices::$enderecoBase . '/api/poi');
        $client->setMethod(Request::METHOD_GET);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $response = $client->send();
        $body = $response->getBody();
        $data = Json::decode($response->getBody(), true);
        return $data;
    }

    # Carrega um Poi por id - GET
    public static function getPoi($id){
        $client = new Client(WebApiServices::$enderecoBase . '/api/poi/' . $id);
        $client->setMethod(Request::METHOD_GET);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $response = $client->send();
        $body = $response->getBody();
        $data = Json::decode($response->getBody(), true);
        return $data;
    }
    

    # Carrega a info do user - GET
    public static function getUserInfo($token){
        $client = new Client(WebApiServices::$enderecoBase . '/api/Account/UserInfo/');
        $client->setMethod(Request::METHOD_GET);
        $bearer_token = 'Bearer ' . $token;
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $response = $client->send();
        $body = $response->getBody();
        $data = Json::decode($response->getBody(), true);
        return $data;
    }
    

    # Cria um Poi - POST
    public static function createPoi($data){
        $client = new Client(WebApiServices::$enderecoBase . '/api/poi/');
        $client->setMethod(Request::METHOD_POST);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $dados = "Description=".$data->descricao."&BusinessHours.FromHour=".$data->horasDe."&BusinessHours.ToHour=".$data->horasPara."&Location.id=".$data->localizacao."&TimeTovisit=".$data->tempoVisita."&Status=".$data->status;
        $client->setRawBody($dados);
        $response = $client->send();
        $body = $response->getBody();
        $data = Json::decode($response->getBody(), true);
        return $data;
    }
    

    # Adiciona Poi ao percurso - POST
    public static function addPoiPercurso($data){
        $client = new Client(WebApiServices::$enderecoBase . '/api/Route/');
        $client->setMethod(Request::METHOD_POST);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $dados = "Order=".$data->Order."&Hour=".$data->Hour."&RunTime=".$data->RunTime."&PoiId=".$data->pois."&VisitId=".$data->id;
        $client->setRawBody($dados);
        $response = $client->send();
        $body = $response->getBody();
        $data = Json::decode($response->getBody(), true);
        return $data;
    }


    # Editar Poi - PUT
    public static function editPoi($data){
        $client = new Client(WebApiServices::$enderecoBase . '/api/poi/' . $data->id);
        $client->setMethod(Request::METHOD_PUT);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $dados = "Description=".$data->descricao."&BusinessHours.FromHour=".$data->horasDe."&BusinessHours.ToHour=".$data->horasPara."&Location.id=".$data->localizacao."&TimeTovisit=".$data->tempoVisita."&Id=".$data->id;
        $client->setRawBody($dados);
        $response = $client->send();
        if ($response->isSuccess()) {
            return true;
        }
        return false;
    }
    


    # Editar Route - POST
    public static function editRoute($data){
        $client = new Client(WebApiServices::$enderecoBase . '/api/Visit/'.$data->id.'/route');
        $client->setMethod(Request::METHOD_POST);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        echo "<pre>";
        
        $array = array(
            '0' => '1',
            '1' => '2'
        ); 

        $dados = "routeids=".json_encode($array);
        $client->setRawBody($dados);
        $response = $client->send();
        print_r($response);
        exit;
        if ($response->isSuccess()) {
            return true;
        }
        return false;
    }
    

    # Apagar Poi - DELETE
    public static function deleteRoute($idRoad){
        $client = new Client(WebApiServices::$enderecoBase.'/api/Route/'.$idRoad);
        
        $client->setMethod(Request::METHOD_DELETE);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $response = $client->send();
        if ($response->isSuccess()) {
            return true;
        }
        return false;
    }
    
    

    # Apagar Percurso - DELETE
    public static function deletePercurso($id){
        $client = new Client(WebApiServices::$enderecoBase.'/api/visit/'.$id.'/route');
        $client->setMethod(Request::METHOD_DELETE);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $response = $client->send();
        echo "<pre>";
        print_r($response);
        exit;
        if ($response->isSuccess()) {
            return true;
        }
        return false;
    }
    
    
    # Apagar Poi - DELETE
    public static function deletePoi($id){
        $client = new Client(WebApiServices::$enderecoBase . '/api/poi/' . $id);
    
        $client->setMethod(Request::METHOD_DELETE);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $client->setHeaders(array(
            'Authorization' => $bearer_token
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $response = $client->send();
        if ($response->isSuccess()) {
            return true;
        }
        return false;
    }
    
    # Aprovar Poi - POST
    public static function aprovePoi($id){
        $client = new Client(WebApiServices::$enderecoBase . '/api/poi/'.$id.'/approve');
        $client->setMethod(Request::METHOD_POST);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $data = "sem dados";
        $len = strlen($data);
        
        $client->setHeaders(array(
            'Content-Type' => 'application/x-www-form-urlencoded',
            'Authorization' => $bearer_token,
            'Content-Length' => $len
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $client->setRawBody($data);
        $response = $client->send();
        if ($response->isSuccess()) {
            return true;
        }
        return false;
    }
    
    # Rejeitar Poi - POST
    public static function rejectPoi($id){
        $client = new Client(WebApiServices::$enderecoBase . '/api/poi/'.$id.'/reject');
        $client->setMethod(Request::METHOD_POST);
        $bearer_token = 'Bearer ' . $_SESSION['token'];
        $data = "sem dados";
        $len = strlen($data);
        
        $client->setHeaders(array(
            'Content-Type' => 'application/x-www-form-urlencoded',
            'Authorization' => $bearer_token,
            'Content-Length' => $len
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $client->setRawBody($data);
        $response = $client->send();
        if ($response->isSuccess()) {
            return true;
        }
        return false;
    }

    
    
    # Login na API - POST
    public static function login($username,$password) {
        $client = new Client(WebApiServices::$enderecoBase . '/token');
        $client->setMethod(Request::METHOD_POST);
        $data = 'grant_type=password&username='.$username.'&password='.$password;
        $len = strlen($data);
        $client->setHeaders(array(
            'Content-Type' => 'application/x-www-form-urlencoded',
            'Content-Length' => $len
        ));
        $client->setOptions([
            'sslverifypeer' => false
        ]);
        $client->setRawBody($data);
        $response = $client->send();
        
        if ($response->isSuccess()) {
            $body = Json::decode($response->getBody());
            if (session_status() == PHP_SESSION_NONE) {
                session_start();
            }
            $resp = WebApiServices::getUserInfo($body->access_token);
            if($resp['Roles'][0]['Name'] == 'Admin') {
                $_SESSION['username'] = $body->userName;
                $_SESSION['token'] = $body->access_token;
                return true;
            }
        }
        return false;
    }
    
    # Logout
    public function logout() {
        if (session_status() == PHP_SESSION_NONE) {
            session_start();
        }
        session_unset();
        session_destroy();
        session_write_close();
        return true;
    }
}

