iwr http://localhost:55162/swagger/v1/swagger.json -o swagger.json -UseDefaultCredential

autorest --input-file=swagger.json --csharp --output-folder=../../Veises.SocialNet.Identity.Contracts/Client --namespace=Veises.SocialNet.Identity.Contracts.Client