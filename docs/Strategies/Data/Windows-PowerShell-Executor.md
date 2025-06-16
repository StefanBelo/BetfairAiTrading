# Windows PowerShell Executor

**Category:** Data  
**Strategy ID:** 2011

## Description

A powerful utility strategy that executes Windows PowerShell commands as part of the trading workflow. This strategy enables integration with external systems, data processing scripts, and automation tasks that extend beyond the core Bfexplorer functionality.

## Parameters

### Data
- **Command** (Required) - The Windows PowerShell command to execute (String)
- **WorkingDirectory** (Required) - The working directory (String)

### Market
- **EvaluateEntryCriteriaOnlyOnce** (Optional) - Set this parameter to True to evaluate the entry criteria only once
- **StopMarketMonitoring** (Optional) - Set this parameter to True to stop market monitoring after the strategy completes execution. Monitoring will stop only if no other bot is active in the market

### Miscellaneous
- **StrategyReference** (Optional) - Set this parameter to specify which strategy sent the bet. The string is limited to 15 characters

## Usage Examples

### Data Export
- **Command**: `"Export-Csv -Path 'C:\Data\market_data.csv' -NoTypeInformation"`
- **WorkingDirectory**: `"C:\Scripts"`

### File Processing
- **Command**: `"Get-ChildItem -Path '*.log' | ForEach-Object { Process-LogFile $_.Name }"`
- **WorkingDirectory**: `"C:\Logs"`

### System Integration
- **Command**: `"Invoke-RestMethod -Uri 'https://api.example.com/webhook' -Method POST -Body $data"`
- **WorkingDirectory**: `"C:\Scripts"`

### Database Operations
- **Command**: `"Invoke-Sqlcmd -Query 'INSERT INTO trades VALUES (...)' -ServerInstance 'localhost'"`
- **WorkingDirectory**: `"C:\Database"`

## Best Practices

1. **Security**: Validate all commands for security implications
2. **Error Handling**: Include error handling in PowerShell scripts
3. **Permissions**: Ensure proper permissions for command execution
4. **Working Directory**: Use absolute paths for reliable execution
5. **Testing**: Thoroughly test commands before live execution
6. **Logging**: Implement logging for executed commands
7. **Resource Management**: Consider system resource usage

## Security Considerations

1. **Input Validation**: Validate all command inputs
2. **Execution Policy**: Ensure appropriate PowerShell execution policy
3. **User Context**: Run with minimal required privileges
4. **Command Injection**: Protect against command injection attacks
5. **File Access**: Limit file system access as needed
