
namespace NavyAccountCore.Models
{
    public enum LoanRegisterEnum
    {
            DNPF_Receives_Application_from_Units,
            Forwarded_To_CPO_for_Validation_or_Eligibility,
            CPO_Return_Verified_Applications_to_DNPF,
            DNPF_Prepares_list_of_qualified_applicants_and_forward_to_NHQ_for_approval,
            NHQ_Returns_Approval_List_for_payment,
            DNPF_Effects_payment_to_approved_beneficiaries,
            DNPF_forward_list_of_loan_beneficiaries_to_CPO_for_monthly_deduction
    }
    
}
