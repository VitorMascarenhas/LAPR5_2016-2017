<?php
namespace TukPorto\Form;
use Zend\Form\Form;

class AddPoiPercursoForm extends Form
{    
    public function __construct($name = null)
    {
         parent::__construct('percurso');

         $this->add(array(
             'name' => 'id',
             'type' => 'Hidden',
         ));
         $this->add(array(
             'name' => 'Order',
             'type' => 'Text',
             'options' => array(
                 'label' => 'Ordem'
             ),
             'attributes' => array(
                 'required' => 'required'
             ),
         ));
         $this->add(array(
             'name' => 'Hour',
             'type' => 'Text',
             'options' => array(
                 'label' => 'Hora de chegada ao Poi',
             ),
             'attributes' => array(
                 'required' => 'required',
                 'pattern' => '([0-9]{1,2}:[0-9]{2})|([0-9]{2}:[0-9]{2}:[0-9]{2})',
                 'title' => '00:00:00 ou 00:00'
             ),
         ));
         $this->add(array(
             'name' => 'RunTime',
             'type' => 'Text',
             'options' => array(
                 'label' => 'Duração do percurso até ao Poi',
             ),
             'attributes' => array(
                 'required' => 'required',
                 'pattern' => '([0-9]{1,2}:[0-9]{2})|([0-9]{2}:[0-9]{2}:[0-9]{2})',
                 'title' => '00:00:00 ou 00:00'
             ),
         ));
         $this->add(array(
             'name' => 'pois',
             'type' => 'Select',
             'options' => array(
                 'label' => 'Poi',
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