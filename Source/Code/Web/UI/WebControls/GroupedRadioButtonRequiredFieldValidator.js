function GroupedRadioButtonRequiredFieldValidator_EvaluateIsValid(source, args)
{
    var group = source.GroupName;    
    var inputs = source.parentNode.getElementsByTagName("input");
    
    for (var index = 0; index < inputs.length; ++index)
    {
        var input = inputs[index];
        if (input.type == "radio" && input.name.endsWith(group) && input.checked == true)
        {
            args.IsValid = true;
            return;
        }
    }
    
    args.IsValid = false;
}