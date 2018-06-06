<?php
namespace TukPorto\Form;
use Zend\Form\Form;

class TuristasForm extends Form
{    
    public function __construct($name = null)
    {
        // we want to ignore the name passed
         parent::__construct('turistas');

         $this->add(array(
             'name' => 'id',
             'type' => 'Hidden',
         ));
         $this->add(array(
             'name' => 'email',
             'type' => 'Text',
             'options' => array(
                 'label' => 'Email',
             ),
         ));
         $this->add(array(
             'name' => 'password',
             'type' => 'password',
             'options' => array(
                 'label' => 'Password',
             ),
         ));
         $this->add(array(
             'name' => 'nacionalidade',
             'type' => 'Text',
             'options' => array(
                 'label' => 'Nacionalidade',
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