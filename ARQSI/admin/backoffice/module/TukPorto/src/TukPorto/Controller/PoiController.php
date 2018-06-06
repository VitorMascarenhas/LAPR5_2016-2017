<?php
namespace TukPorto\Controller;

use Zend\Mvc\Controller\AbstractActionController;
use Zend\View\Model\ViewModel;
use TukPorto\Services\WebApiServices;
use TukPorto\Utils\Utils;
use TukPorto\Model\Poi;
use TukPorto\Form\PoiAddForm;


class PoiController extends AbstractActionController {

# Listagem de Pois
    public function indexAction() {
        if(Utils::verificaSessao() == false){
            return $this->redirect()->toRoute('turistas', array('action' => 'iniciarSessao'));
        }
        
        $pois = WebApiServices::getPois();

        return new ViewModel(array(
            'pois' => $pois,
            'estado' => array(0 => "Pendente", 1 => "Aprovado", 2 => "Rejeitado")
        ));
    }
    
# Cria um novo Poi
    public function addAction() {
        if(Utils::verificaSessao() == false){
            return $this->redirect()->toRoute('turistas', array('action' => 'iniciarSessao'));
        }
        
        $array_localizacoes = WebApiServices::getLocations();
        foreach ($array_localizacoes as $value) {
            $localizacoes[$value['Id']] = $value['Name'];
        }

        $form = new PoiAddForm();
        $form->get('localizacao')->setValueOptions($localizacoes);

        $form->get('submit')->setValue('Adicionar');
        $request = $this->getRequest();
        if ($request->isPost()) {
            $poi = new Poi();
            $form->setInputFilter($poi->getInputFilter());
            $form->setData($request->getPost());
        
            if ($form->isValid()) {
                WebApiServices::createPoi($request->getPost());
                return $this->redirect()->toRoute('poi');
            }
        }
        return array(
            'form' => $form
        );
    }


    # Edita um Poi
    public function editAction() {
        if(Utils::verificaSessao() == false){
            return $this->redirect()->toRoute('turistas', array('action' => 'iniciarSessao'));
        }
    
        $id = (int) $this->params()->fromRoute('id', 0);
        if (!$id) {
            return $this->redirect()->toRoute('poi', array('action' => 'index'));
        }
    
        $data = WebApiServices::getPoi($id);
        $poi = new Poi();
        $poi->exchangeArray($data);
    
        $array_localizacoes = WebApiServices::getLocations();
        foreach ($array_localizacoes as $value) {
            $localizacoes[$value['Id']] = $value['Name'];
        }
    
        $form  = new PoiAddForm();
    
    
        $form->bind($poi);
        $form->get('localizacao')->setValueOptions($localizacoes);
    
        $form->get('submit')->setAttribute('value', 'Editar');
    
        $request = $this->getRequest();
        if ($request->isPost()) {
            $form->setInputFilter($poi->getInputFilter());
            $form->setData($request->getPost());
            if ($form->isValid()) {
                $result = WebApiServices::editPoi($request->getPost());
                if($result == true) {
                    return $this->redirect()->toRoute('poi');
                }
            }
        }
        return array(
            'id' => $id,
            'form' => $form,
        );
    
    }
    
    

    # Ver um Poi
    public function verAction() {
        if(Utils::verificaSessao() == false){
            return $this->redirect()->toRoute('turistas', array('action' => 'iniciarSessao'));
        }
    
        $id = (int) $this->params()->fromRoute('id', 0);
        if (!$id) {
            return $this->redirect()->toRoute('poi', array('action' => 'index'));
        }
    
        $data = WebApiServices::getPoi($id);
        return array(
            'id' => $id,
            'poi' => $data,
            'estado' => array(0 => "Pendente", 1 => "Aprovado", 2 => "Rejeitado")
        );
    
    }
    

# Aprova POI
    public function aprovePoiAction() {
        if(Utils::verificaSessao() == false){
            return $this->redirect()->toRoute('turistas', array('action' => 'iniciarSessao'));
        }
        
        $id = (int) $this->params()->fromRoute('id', 0);
        if ($id>0) {
            $request = $this->getRequest();
            $result = WebApiServices::aprovePoi($id);            
        }

        return $this->redirect()->toRoute('poi', array('action' => 'index'));
    }
    


# Rejeita POI
    public function reprovePoiAction() {
        if(Utils::verificaSessao() == false){
            return $this->redirect()->toRoute('turistas', array('action' => 'iniciarSessao'));
        }
    
        $id = (int) $this->params()->fromRoute('id', 0);
        if ($id>0) {
            $request = $this->getRequest();
            $result = WebApiServices::rejectPoi($id);
        }
        
        return $this->redirect()->toRoute('poi', array('action' => 'index'));
    }


# Apaga um Poi
    public function deleteAction() {
        if(Utils::verificaSessao() == false){
            return $this->redirect()->toRoute('turistas', array('action' => 'iniciarSessao'));
        }
    
        $id = (int) $this->params()->fromRoute('id', 0);
        if (!$id) {
            return $this->redirect()->toRoute('poi', array('action' => 'index'));
        }
        
        $request = $this->getRequest();
        if ($request->isPost()) {
            $del = $request->getPost('del', 'Não');
            
            if ($del == 'Sim') {
                $id = (int) $request->getPost('id');
                $result = WebApiServices::deletePoi($id);
            }
            return $this->redirect()->toRoute('poi');
        }
        return array(
            'id' => $id
        );
    }
    
}
