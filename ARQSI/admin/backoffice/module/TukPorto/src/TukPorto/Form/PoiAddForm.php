<?php
namespace TukPorto\Form;
use Zend\Form\Form;

class PoiAddForm extends Form
{    
    public function __construct($name = null)
    {
         parent::__construct('poi');

         $this->add(array(
             'name' => 'id',
             'type' => 'Hidden',
         ));
         $this->add(array(
             'name' => 'descricao',
             'type' => 'Text',
             'options' => array(
                 'label' => 'Descrição'
             ),
             'attributes' => array(
                 'required' => 'required'
             ),
         ));
         $this->add(array(
             'name' => 'horasDe',
             'type' => 'Text',
             'options' => array(
                 'label' => 'Hora de abertura',
             ),
             'attributes' => array(
                 'required' => 'required',
                 'pattern' => '[0-9]{2}:[0-9]{2}:[0-9]{2}',
                 'title' => '00:00:00'
             ),
         ));
         $this->add(array(
             'name' => 'horasPara',
             'type' => 'Text',
             'options' => array(
                 'label' => 'Hora de fecho',
             ),
             'attributes' => array(
                 'required' => 'required',
                 'pattern' => '[0-9]{2}:[0-9]{2}:[0-9]{2}',
                 'title' => '00:00:00'
             ),
         ));
         $this->add(array(
             'name' => 'localizacao',
             'type' => 'Select',
             'options' => array(
                 'label' => 'Localização',
             ),
         ));
         $this->add(array(
             'name' => 'tempoVisita',
             'type' => 'Text',
             'options' => array(
                 'label' => 'Tempo de Visita',
             ),
             'attributes' => array(
                 'required' => 'required',
                 'pattern' => '([0-9]+)|([0-9]+.[0-9]+)',
                 'title' => '00.00 ou 00'
             ),
         ));
         $this->add(array(
             'name' => 'status',
             'type' => 'Hidden',
             'attributes' => array(
                 'value' => '0',
             ),
             'options' => array(
                 'label' => 'Estado',
             ),
         ));
         $this->add(array(
             'name' => 'submit',
             'type' => 'Submit',
             'attributes' => array(
                 'value' => 'Go',
                 'id' => 'submitbutton',
                 'class' => 'botao',
             ),
         ));
     }
    
}