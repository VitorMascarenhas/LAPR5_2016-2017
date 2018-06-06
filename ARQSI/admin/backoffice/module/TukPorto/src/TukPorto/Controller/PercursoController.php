<?php
namespace TukPorto\Controller;

use Zend\Mvc\Controller\AbstractActionController;
use Zend\View\Model\ViewModel;
use TukPorto\Services\WebApiServices;
use TukPorto\Utils\Utils;
use TukPorto\Model\Percurso;
use TukPorto\Form\AddPoiPercursoForm;


class PercursoController extends AbstractActionController {

    # Listagem de Percursos
    public function indexAction() {
        if(Utils::verificaSessao() == false){
            return $this->redirect()->toRoute('turistas', array('action' => 'iniciarSessao'));
        }
        
        $percursos = WebApiServices::getPercursos();

        return new ViewModel(array(
            'percursos' => $percursos
        ));
    }

    # Ver um Percurso
    public function verAction() {
        if(Utils::verificaSessao() == false){
            return $this->redirect()->toRoute('turistas', array('action' => 'iniciarSessao'));
        }
    
        $id = (int) $this->params()->fromRoute('id', 0);
        if (!$id) {
            return $this->redirect()->toRoute('percurso', array('action' => 'index'));
        }
    
        $data = WebApiServices::getPercurso($id);
        $route = Utils::orderArray($data['Route']);

        return array(
            'id' => $id,
            'percurso' => $data,
            'route' => $route
        );
    }
    

    # Adiciona um Poi ao percurso
    public function addPoiPercursoAction() {
        if(Utils::verificaSessao() == false){
            return $this->redirect()->toRoute('turistas', array('action' => 'iniciarSessao'));
        }
        
        $id = (int) $this->params()->fromRoute('id', 0);
        
        $array_poi = WebApiServices::getPois();
        foreach ($array_poi as $value) {
            $opcoes[$value['Id']] = $value['Description'];
        }
        
        $form = new AddPoiPercursoForm();
        
        $form->get('pois')->setValueOptions($opcoes);
        $form->get('submit')->setValue('Adicionar');
        $form->get('id')->setValue($id);

        $request = $this->getRequest();
        if ($request->isPost()) {
            $form->setData($request->getPost());
            if ($form->isValid()) {
                WebApiServices::addPoiPercurso($request->getPost());
                return $this->redirect()->toRoute('percurso', array('action' => 'ver','id' => $id));
            }
        }
        return array(
            'id' => $id,
            'form' => $form
        );
    }

    # Apaga um poi de um Percurso de uma Visita
    public function deleteRouteAction() {
        if(Utils::verificaSessao() == false){
            return $this->redirect()->toRoute('turistas', array('action' => 'iniciarSessao'));
        }
    
        $id = (int) $this->params()->fromRoute('id', 0);
    
        if (!$id) {
            return $this->redirect()->toRoute('percurso', array('action' => 'index'));
        }
    
        $request = $this->getRequest();
    
        if ($request->isPost()) {
            $del = $request->getPost('del', 'Não');
            if ($del == 'Sim') {
                $id_d = (int) $request->getPost('id');
                $result = WebApiServices::deleteRoute($id_d);
            }
            return $this->redirect()->toRoute('percurso');
        }
        return array(
            'id' => $id
        );
    }

    
    
}
