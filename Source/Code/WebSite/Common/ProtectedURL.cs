using System;
using System.Collections.Specialized;
using System.Text;


/// 2007-09-12 AORL
///	primeira versão; incorporado na classe geral
public class ProtectedURL
{
    // o formato (descodificado) de um URL é o seguinte:
    // 
    // inicio:
    //   <random><separador>
    //   <versao><separador>
    //   <data><separador>
    //   <numparams><separador>
    //   [
    //      <paramN><separador>
    //      <sizeN><separador>
    //      <valueN(sizeN)>
    //   ]
    // fim:
    //  <checksum><separador>
    //  <random>
    // 
    // <random>        "n" caracteres aleatórios (caracteres alfanuméricos)
    // <separador>     caracter separador (pré-definido, não pode ser alfanumérico)
    // <versao>        número real que indica a versão/revisão da aplicação
    // <data>	       data de criação do URL no formato YYYYMMDDhhmmss
    // <numparams>     número de parâmetros codificados
    // <paramN>        nome do parâmetro N (qualquer caracter excepto o separador)
    // <sizeN>         tamanho do parametro N em caracteres
    // <valueN(sizeN)> "sizeN" caracteres de informação
    // <checksum>      código simples (soma aritmética de todos os caracteres entre "inicio" e "fim") para verficar a integridade dos dados

    public const string VERSION = "1.0";
    public const int MIN_RANDOM = 4;
    public const int MAX_RANDOM = 8;
    public const byte SEPARATOR = 10;
    public const string BASE64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

    private DateTime _data;
    private ASCIIEncoding _ascii;
    private NameValueCollection _parametros;

    /// <summary>
    /// Number of parameters in the URL
    /// </summary>
    public int NumberParameters
    {
        get { return _parametros.Count; }
    }

    /// <summary>
    /// Date when the URL was created
    /// </summary>
    public DateTime CreationDate
    {
        get { return _data; }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public ProtectedURL()
    {
        _data = DateTime.Now;
        _ascii = new ASCIIEncoding();
        _parametros = new NameValueCollection();
    }

    public NameValueCollection Parameters
    {
        get { return this._parametros; }
    }

    /// <summary>
    /// cifra/decifra uma tabela de bytes com o algoritmo RC4
    /// </summary>
    private byte[] RC4(byte[] tabela, byte[] password, bool decifrar)
    {
        byte t;
        int i, j, k;

        // criar a tabela para o resultado
        byte[] resultado = new byte[tabela.Length];

        // criar e inicializar a tabela de permutações do RC4 com a password
        byte[] tabRC4 = new byte[256];
        for (i = 0; i < 256; i++)
            tabRC4[i] = Convert.ToByte(i);

        j = 0;
        for (i = 0; i < 256; i++)
        {
            // calcular o próximo índice para a troca
            j = ((j + Convert.ToInt32(tabRC4[i]) + Convert.ToInt32(password[i % password.Length])) & 255);

            // trocar os elementos
            t = tabRC4[i];
            tabRC4[i] = tabRC4[j];
            tabRC4[j] = t;
        }

        // construir o resultado final
        j = 0;
        k = 0;
        for (i = 0; i < tabela.Length; i++)
        {
            j = ((j + 1) & 255);
            k = ((k + Convert.ToInt32(tabRC4[j])) & 255);

            t = tabRC4[j];
            tabRC4[j] = tabRC4[k];
            tabRC4[k] = t;

            resultado[i] = Convert.ToByte(tabela[i] ^ tabRC4[((Convert.ToInt32(tabRC4[j]) + Convert.ToInt32(tabRC4[k])) & 255)]);

            if (i > 0)
            {
                if (decifrar)
                    resultado[i] ^= Convert.ToByte(tabela[i - 1]);
                else
                    resultado[i] ^= Convert.ToByte(resultado[i - 1]);
            }
            else
                resultado[i] ^= Convert.ToByte(165);
        }

        return resultado;
    }

    /// <summary>
    /// procura pelo separador numa tabela 
    /// </summary>
    private int ProcurarSeparador(byte[] tabela, int inicio)
    {
        for (int i = inicio; i < tabela.Length; i++)
            if (tabela[i] == ProtectedURL.SEPARATOR)
                return i;

        return -1;
    }

    /// <summary>
    /// extrai um texto da tabela de bytes e actualiza o índice de inicio
    /// </summary>
    private string ExtrairTexto(byte[] tabela, ref int inicio)
    {
        if (inicio < tabela.Length)
        {
            // procurar pelo separador
            int i = ProcurarSeparador(tabela, inicio);
            if (i >= inicio)
            {
                // extrair o texto e actualizar o indice
                string texto = _ascii.GetString(tabela, inicio, i - inicio);
                inicio = i + 1;
                return texto;
            }
        }

        return "";
    }

    /// <summary>
    /// extrai um número inteiro não negativo da tabela de bytes e actualiza o índice de inicio 
    /// </summary>
    private int ExtrairNumero(byte[] tabela, ref int inicio)
    {
        int numero = Int32.Parse(ExtrairTexto(tabela, ref inicio));
        if (numero < 0)
            throw new Exception();

        return numero;
    }

    /// <summary>
    /// gera um texto aleatório
    /// </summary>
    private string GerarTextoAleatorio()
    {
        // obter objecto para números aleatórios
        Random rnd = new Random();

        // determinar o tamanho
        int tam = MIN_RANDOM + Convert.ToInt32(rnd.Next(MAX_RANDOM - MIN_RANDOM));

        // construir o resultado
        string resultado = "";
        for (; tam > 0; tam--)
            resultado += BASE64.Substring(rnd.Next(61), 1);

        return resultado;
    }

    /// <summary>
    /// Remove all parameters from the URL
    /// </summary>
    public void RemoveAllParameters()
    {
        _parametros.Clear();
    }

    /// <summary>
    /// Remove a parameter from the URL
    /// </summary>
    /// <param name="name">Name of the parameter to remove</param>
    public void RemoveParameter(string name)
    {
        _parametros.Remove(name);
    }

    /// <summary>
    /// Add a parameter to the URL
    /// </summary>
    /// <returns>True if the parameter was added or false otherwise</returns>
    public bool AddParameter(string name, string value)
    {
        // o nome do parâmetro não pode incluir o separador
        if (name.IndexOf(Convert.ToChar(SEPARATOR)) >= 0)
            return false;

        // o parâmetros já existe ?
        if (_parametros[name] == null)
            _parametros.Add(name, value);
        else
            _parametros[name] = value;

        return true;
    }

    /// <summary>
    /// Get the name of a parameter in the URL
    /// </summary>
    /// <param name="index">Parameter index</param>
    /// <returns>Name or "" if the parameter does not exist</returns>
    public string GetParameterName(int index)
    {
        if (index < 0 || index >= _parametros.Count)
            return "";

        return _parametros.GetKey(index);
    }

    /// <summary>
    /// Get the value of a parameter in the URL
    /// </summary>
    /// <param name="index">Parameter index</param>
    /// <returns>Value or "" if the parameter does not exist</returns>
    public string GetParameterValue(int index)
    {
        if (index < 0 || index >= _parametros.Count)
            return "";

        return _parametros[index];
    }

    /// <summary>
    /// Get the value of a parameter in the URL
    /// </summary>
    /// <param name="name">Name of the paramter</param>
    /// <returns>Value or "" if the parameter does not exist</returns>
    public string GetParameterValue(string name)
    {
        if (_parametros[name] == null)
            return "";

        return _parametros[name];
    }

    /// <summary>
    /// Encode the URL using a password
    /// </summary>
    /// <returns>Text that represents the encoded URL</returns>
    public string Encode(string password)
    {
        // construir o texto que vai ser codificado
        string texto = "";
        string separador = Convert.ToChar(SEPARATOR).ToString();

        // inicio:
        //   <random><separador>
        texto += (GerarTextoAleatorio() + separador);

        //   <versao><separador>
        texto += (VERSION + separador);

        //   <data><separador>
        texto += (_data.ToString("yyyyMMddhhmmss") + separador);

        //   <numparams><separador>
        texto += (_parametros.Count + separador);

        //   [
        foreach (string p in _parametros)
        {
            //      <paramN><separador>
            texto += (p + separador);

            //      <sizeN><separador>
            texto += (_parametros[p].Length + separador);

            //      <valueN(sizeN)>
            texto += _parametros[p];
        }
        //   ]

        // fim:
        // calcular a soma aritmética de todos os caracteres entre "inicio" e "fim"
        int soma = 0;
        char[] caracteres = texto.ToCharArray();
        for (int i = 0; i < caracteres.Length; i++)
            soma += Convert.ToInt32(caracteres[i]);

        //  <checksum><separador>
        texto += (soma.ToString() + separador);

        //  <random>
        texto += GerarTextoAleatorio();

        // vamos codificar o texto e devolver a codificação em base 64
        byte[] resultado = RC4(_ascii.GetBytes(texto), _ascii.GetBytes(password), false);
        return Convert.ToBase64String(resultado);
    }

    /// <summary>
    /// Decode the URL using a password
    /// </summary>
    /// <param name="text">Text with the encoded URL</param>
    /// <returns>True if the URL was successfully decoded or False otherwise</returns>
    public bool Decode(string text, string password)
    {
        try
        {
            // descodificar o texto com a password fornecida
            byte[] tabela = RC4(Convert.FromBase64String(text), _ascii.GetBytes(password), true);

            // inicio:
            //   <random><separador>
            int i = 0;
            text = ExtrairTexto(tabela, ref i);
            if (text.Length < MIN_RANDOM || text.Length > MAX_RANDOM)
                throw new Exception();

            //   <versao><separador>
            if (ExtrairTexto(tabela, ref i) != VERSION)
                throw new Exception();

            //   <data><separador>
            text = ExtrairTexto(tabela, ref i);
            _data = new DateTime(
                Convert.ToInt32(text.Substring(0, 4)),
                Convert.ToInt32(text.Substring(4, 2)),
                Convert.ToInt32(text.Substring(6, 2)),
                Convert.ToInt32(text.Substring(8, 2)),
                Convert.ToInt32(text.Substring(10, 2)),
                Convert.ToInt32(text.Substring(12, 2)));

            //   <numparams><separador>
            int numParams = ExtrairNumero(tabela, ref i);
            _parametros.Clear();

            // ler os parâmetros ?
            //   [
            for (; numParams > 0; numParams--)
            {
                //      <paramN><separador>
                string nome = ExtrairTexto(tabela, ref i);
                if (nome.Length == 0 || _parametros[nome] != null)
                    throw new Exception();

                //      <sizeN><separador>
                int tamanho = ExtrairNumero(tabela, ref i);
                if (tamanho > 0)
                {
                    // existe informação suficiente para o valor do parâmetro ?
                    if (i + tamanho >= tabela.Length)
                        throw new Exception();

                    //      <valueN(sizeN)>
                    _parametros.Add(nome, _ascii.GetString(tabela, i, tamanho));
                    i += tamanho;
                }
            }
            //   ]

            // fim:
            // calcular a soma aritmética de todos os caracteres (bytes) entre "inicio" e "fim"
            int soma = 0;
            for (int j = 0; j < i; j++)
                soma += Convert.ToInt32(tabela[j]);

            //  <checksum><separador>
            if (ExtrairNumero(tabela, ref i) != soma)
                throw new Exception();

            //  <random>
            text = _ascii.GetString(tabela, i, tabela.Length - i);
            if (text.Length < MIN_RANDOM || text.Length > MAX_RANDOM)
                throw new Exception();

            return true;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            // o URL é inválido
            _data = DateTime.MinValue;
            _parametros.Clear();
            return false;
        }
    }
}