<?php
return array(
    'controllers' => array(
        'invokables' => array(
            'TukPorto\Controller\Turistas' => 'TukPorto\Controller\TuristasController',
            'TukPorto\Controller\Poi' => 'TukPorto\Controller\PoiController',
            'TukPorto\Controller\Percurso' => 'TukPorto\Controller\PercursoController'
        )
    ),
    'router' => array(
        'routes' => array(
            'turistas' => array(
                'type' => 'segment',
                'options' => array(
                    // Change this to something specific to your module
                    'route' => '/turistas[/:action][/:id]',
                    'constraints' => array(
                        'action' => '[a-zA-Z][a-zA-Z0-9_-]*',
                        'id' => '[0-9]+'
                    ),
                    'defaults' => array(
                        'controller' => 'TukPorto\Controller\Turistas',
                        'action' => 'index'
                    )
                )
            ),
            'poi' => array(
                'type' => 'segment',
                'options' => array(
                    // Change this to something specific to your module
                    'route' => '/poi[/:action][/:id]',
                    'constraints' => array(
                        'action' => '[a-zA-Z][a-zA-Z0-9_-]*',
                        'id' => '[0-9_]+'
                    ),
                    'defaults' => array(
                        'controller' => 'TukPorto\Controller\Poi',
                        'action' => 'index'
                    )
                )
            ),
            'percurso' => array(
                'type' => 'segment',
                'options' => array(
                    // Change this to something specific to your module
                    'route' => '/percurso[/:action][/:id][/:idPercurso][/:idPoi]',
                    'constraints' => array(
                        'action' => '[a-zA-Z][a-zA-Z0-9_-]*',
                        'id' => '[0-9_]+',
                        'idPercurso' => '[0-9_]+',
                        'idPoi' => '[0-9_]+'
                    ),
                    'defaults' => array(
                        'controller' => 'TukPorto\Controller\Percurso',
                        'action' => 'index'
                    )
                )
            )
            
        )
    ),
    'view_manager' => array(
        'template_path_stack' => array(
            'TukPorto' => __DIR__ . '/../view'
        )
    )
);
