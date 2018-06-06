<?php
/**
 * Zend Framework (http://framework.zend.com/)
 *
 * @link      http://github.com/zendframework/ZendSkeletonApplication for the canonical source repository
 * @copyright Copyright (c) 2005-2015 Zend Technologies USA Inc. (http://www.zend.com)
 * @license   http://framework.zend.com/license/new-bsd New BSD License
 */
namespace Application\Controller;

use Zend\Mvc\Controller\AbstractActionController;
use Zend\View\Model\ViewModel;
use TukPorto\Services\WebApiServices;

class IndexController extends AbstractActionController
{

    public function indexAction()
    {
        if (session_status() == PHP_SESSION_NONE) {
            session_start();
        }
        if (! isset($_SESSION['username'])) {
            return $this->redirect()->toRoute('turistas', array(
                'action' => 'iniciarSessao'
            ));
        }
        
        $stats = WebApiServices::getStats();
        return new ViewModel(array(
            'total_pois_rejeitados' => $stats['CountOfRejectedPoi'],
            'total_pois_aprovados' => $stats['CountOfApprovedPoi'],
            'total_pois_pendentes' => $stats['CountOfPendingPoi'],
            'total_pois' => $stats['CountOfPoi'],
            'total_users' => $stats['CountOfUsers']
        ));
    }
}
