<?php
namespace TukPorto\Model;

use Zend\InputFilter\InputFilter;
use Zend\InputFilter\InputFilterAwareInterface;
use Zend\InputFilter\InputFilterInterface;

class Percurso implements InputFilterAwareInterface {

    public $Name;
    public $StartDate;
    public $Enddate;
    public $User;
    public $Duration;
    public $PointsOfInterests;
    
    
    protected $inputFilter;
    
    public function exchangeArray($data) {
        $this->id       = (!empty($data['Id'])) ? $data['Id'] : null;
        $this->Name   = (!empty($data['Name'])) ? $data['Name'] : null;
        $this->StartDate   = (!empty($data['StartDate'])) ? $data['StartDate'] : null;
        $this->Enddate   = (!empty($data['Enddate'])) ? $data['Enddate'] : null;
        $this->User   = (!empty($data['User']['UserName'])) ? $data['User']['UserName'] : null;
        $this->Duration   = (!empty($data['Duration'])) ? $data['Duration'] : null;
        $this->PointsOfInterests   = (!empty($data['PointsOfInterests'])) ? $data['PointsOfInterests'] : null;
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
                 'name'     => 'Name',
                 'required' => false
             ));
             
             $inputFilter->add(array(
                 'name'     => 'StartDate',
                 'required' => false
             ));
             
             $inputFilter->add(array(
                 'name'     => 'Enddate',
                 'required' => false
             ));
             
             $inputFilter->add(array(
                 'name'     => 'User',
                 'required' => false
             ));
             
             $inputFilter->add(array(
                 'name'     => 'Duration',
                 'required' => false
             ));
             
             $inputFilter->add(array(
                 'name'     => 'PointsOfInterests',
                 'required' => false
             ));

             $this->inputFilter = $inputFilter;
         }

         return $this->inputFilter;
     }
        
}