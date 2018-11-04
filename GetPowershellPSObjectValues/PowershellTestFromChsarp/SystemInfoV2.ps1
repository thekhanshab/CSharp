
$propertiesHardWare = @{
    RAM = "1GB"
    Processor = "Core i5"
    HardDisk = "1TB"
}

$propertiesSystem = @{
    WebSites = "Default Web Site"
    AppPools = "Dot Net 2.0"
    InstalledDotNet = "2.0, 3.5, 4.0, 4.5"
    RunTimeDotNet = "4.5"
    MachineConfig = "machineConfig.xml"
    HardWare = $propertiesHardWare
}

return New-Object PSObject -Property $propertiesSystem