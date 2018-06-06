[string] $Server= "10.8.11.147\SQLEXPRESS"
[string] $Database = "PortoGO"
[string] $user = "porto"
[string] $pwd = "porto"
[string] $UserSqlQuery= $("SELECT UserName FROM [dbo].[AspNetUsers]")

#variaveis para guardar a indicação do retorno do script para o nagios
$resW = 0
$resC = 0

function ExecuteSqlQuery ($Server, $Database, $SQLQuery) {
    $Datatable = New-Object System.Data.DataTable

    $Connection = New-Object System.Data.SQLClient.SQLConnection
    $Connection.ConnectionString = "server='$Server';database='$Database';User Id='$user';Password=$pwd;"
    $Connection.Open()
    $Command = New-Object System.Data.SQLClient.SQLCommand
    $Command.Connection = $Connection
    $Command.CommandText = $SQLQuery
    $Reader = $Command.ExecuteReader()
    $Datatable.Load($Reader)
    $Connection.Close()

    return $Datatable
}

$usersDataTable = New-Object System.Data.DataTable
$usersDataTable = ExecuteSqlQuery $Server $Database $UserSqlQuery 

$usersList = @() #array com os utilizadores da BD para comparação
$usersDataTable |  FOREACH-OBJECT {
    $usersList += $_.UserName
}

$shareFolders = get-WmiObject -class Win32_Share -computer localhost #lista as shares da maquina. equivalente ao net share

$shareList = @() #array para guardar o nome da share
$shareFolders | FOREACH-OBJECT {
    if($_.Path.StartsWith("C:\Share\","CurrentCultureIgnoreCase") )
    {
        $shareList += $_.Path.SubString($_.Path.LastIndexOf("\") + 1)
    }
}

$folderList = @() #array com o nome da pasta partilhada
$folders = Get-ChildItem c:\share -Recurse | ?{ $_.PSIsContainer } 
$folders | FOREACH-OBJECT {
    $folderList += $_.FullName.SubString($_.FullName.LastIndexOf("\") + 1)
}

#comparar shares com pastas criadas
$result = Compare-Object -ReferenceObject $folderList -DifferenceObject $shareList -PassThru   

if ($result.Count -ne 0) 
{
    if($folderList.Count -gt $shareList.Count)
    {
        echo ("CRITICAL: Existem pastas criadas que n estão partilhadas!")

        $resC = 1
    }

    if($shareList.Count -gt $folderList.Count)
    {
        echo ("WARNING: Existem shares criadas para pastas nao existentes!")

        $resW = 1
    }
}

#comparar utilizadores da bd com shares
$result = Compare-Object -ReferenceObject $usersList -DifferenceObject $shareList # -PassThru   

if ($result -ne $null -and $result.Count -ne 0) 
{
    echo ("Existem diferencas entre o numero de utilizadores da bd e o numero de pastas partilhadas")

    $userWithoutFolder = $result | Where-Object { $_.SideIndicator -eq '<=' }
    if($userWithoutFolder -ne $null)
    {
        echo ("CRITICAL: Utilizadores sem pasta:")

        foreach($r in $userWithoutFolder)
        {
            echo ($r.InputObject)
        }

        $resC = 1
    }

    $folderWithoutUser = $result | Where-Object { $_.SideIndicator -eq '=>' }

    if($folderWithoutUser -ne $null)
    {
        echo ("WARNING: Pastas sem utilizador:")

        foreach($r in $folderWithoutUser)
        {
            echo ("C:\Share\" + $r.InputObject)
        }

        $resW = 1
    }
}

#decidir se vamos retornar OK, WARNING ou CRITICAL
if($resC -eq 1)
{
    exit 2 #CRITICAL
}

if($resW -eq 1)
{
    exit 1 #WARNING
}

echo ("OK: okidoki!")

exit 0
