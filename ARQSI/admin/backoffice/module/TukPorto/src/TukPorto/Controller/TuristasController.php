<?php
/**
 * Zend Framework (http://framework.zend.com/)
 *
 * @link      http://github.com/zendframework/Turistas for the canonical source repository
 * @copyright Copyright (c) 2005-2015 Zend Technologies USA Inc. (http://www.zend.com)
 * @license   http://framework.zend.com/license/new-bsd New BSD License
 */
namespace TukPorto\Controller;

use Zend\Mvc\Controller\AbstractActionController;
use Zend\View\Model\ViewModel;
use TukPorto\Model\Turistas;
use TukPorto\Form\LoginForm;
use Zend\Http\Request;
use TukPorto\Services\WebApiServices;
use TukPorto\Utils\Utils;

class TuristasController extends AbstractActionController {

    # Listagem de utilizadores
    public function indexAction() {
        if(Utils::verificaSessao() == false){
            return $this->redirect()->toRoute('turistas', array('action' => 'iniciarSessao'));
        }
        
        $users = WebApiServices::getUsers();

        return new ViewModel(array(
            'turistas' => $users
        ));
    }

    # Formulário de login
    public function iniciarSessaoAction() {
        $form = new LoginForm();
        $form->get('submit')->setValue('Iniciar sessão');
        return array(
            'form' => $form
        );
    }

    # Verifica login
    public function loginAction() {
        $request = $this->getRequest();
        
        if ($request->isPost()) {
            $username = $request->getPost('email');
            $password = $request->getPost('password');
            $result = WebApiServices::login($username, $password);
            if($result == true) {
                return $this->redirect()->toRoute('home');
            } else {
                return $this->redirect()->toRoute('home', array(
                    'controller' => 'Turistas',
                    'action' => 'iniciarSessao'
                ));
            }
        } else {
            $form = new LoginForm();
            $form->get('submit')->setValue('Login');
            return array(
                'form' => $form
            );
        }
    }

    # Faz logout
    public function logoutAction() {
        $result = WebApiServices::logout();
        if(result == true) {
            return $this->redirect()->toRoute('turistas', array(
                'action' => 'iniciarSessao'
            ));            
        }
    }
}