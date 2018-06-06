<?php
namespace TukPorto\Model;

use Zend\InputFilter\InputFilter;
use Zend\InputFilter\InputFilterAwareInterface;
use Zend\InputFilter\InputFilterInterface;

class Poi implements InputFilterAwareInterface {

    public $descricao;
    public $horasDe;
    public $horasPara;
    public $localizacao;
    public $tempoVisita;
    public $status;
    
    protected $inputFilter;
    
    public function exchangeArray($data) {
        $this->id       = (!empty($data['Id'])) ? $data['Id'] : null;
        $this->descricao   = (!empty($data['Description'])) ? $data['Description'] : null;
        $this->horasDe   = (!empty($data['BusinessHours']['FromHour'])) ? $data['BusinessHours']['FromHour'] : null;
        $this->horasPara   = (!empty($data['BusinessHours']['ToHour'])) ? $data['BusinessHours']['ToHour'] : null;
        $this->localizacao   = (!empty($data['Location']['Id'])) ? $data['Location']['Id'] : null;
        $this->tempoVisita   = (!empty($data['TimeTovisit'])) ? $data['TimeTovisit'] : null;
        $this->status   = (!empty($data['Status'])) ? $data['Status'] : null;
    }

    public function getArrayCopy() {
        return get_object_vars($this);
    }

    public function setInputFilter(InputFilterInterface $inputFilter) {
        throw new \Exception("Not used");    
    }

    public function getInputFilter() {
       if (!$this->inputFilter) {
             $inputFilter = new InputFilter();

             $inputFilter->add(array(
                 'name'     => 'id',
                 'required' => false,
                 'filters'  => array(
                     array('name' => 'Int'),
                 ),
             ));
             
             $inputFilter->add(array(
                 'name'     => 'descricao',
                 'required' => false
             ));
             
             $inputFilter->add(array(
                 'name'     => 'horasDe',
                 'required' => false
             ));
             
             $inputFilter->add(array(
                 'name'     => 'horasPara',
                 'required' => false
             ));
             
             $inputFilter->add(array(
                 'name'     => 'localizacao',
                 'required' => false
             ));
             
             $inputFilter->add(array(
                 'name'     => 'tempoVisita',
                 'required' => false
             ));
             
             $inputFilter->add(array(
                 'name'     => 'status',
                 'required' => false
             ));

             $this->inputFilter = $inputFilter;
         }

         return $this->inputFilter;
     }
        
}