# Call Center

## Dependencies

* Microsoft.Extensions.Configuration
* Microsoft.Extensions.Configuration.FileExtensions
* Microsoft.Extensions.Configuration.Json

## Configuration file description

Configuration is named "appsettings.json". It's written in Json Format and has next view:
<pre>
	<code>
{
  "parameters": {
    "managersCount": 3,
    "operatorsCount": 2
  }
}
	</code>
</pre>


Parameter <code>managersCount</code> set up how much operators will be work in Call Center and <code>operatorsCount</code> parameter set up same thing for operators.  Parameters have to be a positive integer value.