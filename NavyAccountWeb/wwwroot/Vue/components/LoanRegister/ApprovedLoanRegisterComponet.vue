<template>
    <!-- WRAPPER -->

    <div v-if="postBody.fullName">

        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-header">
                        <h5 class="card-title">Final Loan Update</h5>
                    </div>

                    <div v-if="errors" class="has-error"> {{ [errors] }}</div>
                    <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
                    <form @submit="checkForm" action="/LoanType/CreateLoanType" method="post">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Full Name</label>
                                        <input type="text" name="fullName" class="form-control" v-model="postBody.fullName" readonly />
                                    </div>
                                </div>
                                <input type="hidden" name="personID" class="form-control" v-model="postBody.personID" readonly />
                                <input type="hidden" name="loanTypeID" class="form-control" v-model="postBody.loanTypeID" readonly />
                                <input type="hidden" name="BankID" class="form-control" v-model="postBody.BankID" readonly />

                                <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Loan App Number</label>

                                        <input type="text" name="loanAppNo" class="form-control" v-model="postBody.loanAppNo" readonly />

                                    </div>
                                </div>

                                <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Loan Type</label>

                                        <input type="text" name="loanType" class="form-control" v-model="postBody.loanTypeDesc" readonly />

                                    </div>
                                </div>
                                <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Amount</label>

                                        <input type="text" name="Amount" class="form-control" v-model="postBody.Amount" readonly />

                                    </div>
                                </div>
                                <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Tenure</label>
                                        <input class="form-control" name="Tenure" v-model="postBody.Tenure" placeholder="" readonly />
                                    </div>
                                </div>

                                <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Status</label>
                                        <input class="form-control" name="status" v-model="postBody.status" placeholder="" readonly />
                                    </div>
                                </div>

                                <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Approval Date</label>
                                        <vuejsDatepicker v-model="postBody.ApproveDate" :disabledDates="disabledDates" input-class="form-control" name="approvalDate"></vuejsDatepicker>
                                    </div>
                                </div>

                                ________________________________________________________________________________________________________________________________________________________
                                <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Cheque No</label>
                                        <input class="form-control" name="chequeno" v-model="postBody.ChequeNo" placeholder="" />
                                    </div>
                                </div>
                                <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Cheque Date</label>
                                        <vuejsDatepicker v-model="postBody.EffectiveDate" input-class="form-control" name="effectiveDate"></vuejsDatepicker>
                                    </div>
                                </div>

                                <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Bank</label>
                                        <select class="form-control" v-model="postBody.BankID" name="BankID" required>
                                            <option v-for="bank in banktList" v-bind:value="bank.acctcode" v-bind:key="bank.acctcode"> {{ bank.description }} </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Status And Date</label>
                                        <textarea class="form-control" name="chequeno" v-model="postBody.StatusAndStatusDate" placeholder="" />
                                    </div>
                                </div>

                                <div class="col-12 col-xl-6">
                                    <div class="form-group">
                                        <label class="form-label">Remarks</label>
                                        <input class="form-control" name="remarks" v-model="postBody.remarks" placeholder="" />
                                    </div>
                                </div>

                                <div class="col-12 ">
                                    <div class="btn-group mr-2 sw-btn-group-extra" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="button">{{submitorUpdate}}</button></div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>

    </div>
    <!-- END WRAPPER -->
</template>

<script>
    import vuejsDatepicker from 'vuejs-datepicker'
    export default {
        components: {
            vuejsDatepicker
        },
         
        data() {
            return {
                errors: null,
                responseMessage: '',
                banktList: null,
                submitorUpdate: 'Submit',
               disabledDates: {
                      to: new Date(Date.now() - 8640000)
                    },
                noapproval: false,
                postBody: {
                    fullName: '',
                    loanTypeDesc: '',
                    Tenure: '',
                    ApproveDate: null,
                    Amount: '',
                    ChequeNo:'',
                    status: '',
                    statusAndDate: null,
                    BankID: null,
                    loanTypeID: null,
                    fundTypeID:null,
                    EffectiveDate: null,
                    remarks: '',
                    personID: 0,
                    loanAppNo:'',
                    id: 0
                }

            }
        },
        watch: {
            '$store.state.objectToUpdate': function (newVal, oldVal) {
                this.postBody.fullName = this.$store.state.objectToUpdate.firstName + " " + this.$store.state.objectToUpdate.lastName + " " + this.$store.state.objectToUpdate.middleName,
                    this.postBody.loanTypeDesc = this.$store.state.objectToUpdate.loanTypeDesc,
                    this.postBody.Tenure = this.$store.state.objectToUpdate.tenure,
                    this.postBody.status = this.$store.state.objectToUpdate.status,
                    this.postBody.Amount = this.$store.state.objectToUpdate.amount,
                    this.postBody.remarks = this.$store.state.objectToUpdate.remarks,
                    this.postBody.ApproveDate = this.$store.state.objectToUpdate.approveDate,
                    this.postBody.ChequeNo = this.$store.state.objectToUpdate.chequeNo,
                    this.postBody.id = this.$store.state.objectToUpdate.id,
                    this.postBody.personID = this.$store.state.objectToUpdate.personID,
                    this.postBody.BankID = this.$store.state.objectToUpdate.bankID,
                    this.postBody.loanTypeID = this.$store.state.objectToUpdate.loanTypeID,
                     this.postBody.loanAppNo = this.$store.state.objectToUpdate.loanAppNo,
                    
                     this.postBody.EffectiveDate = this.$store.state.objectToUpdate.effectiveDate,
                this.postBody.StatusAndStatusDate = this.$store.state.objectToUpdate.statusAndDate

                this.submitorUpdate = 'Update';

            }


        },
        mounted() {
            this.$store.state.objectToUpdate = null,


                axios
                    .get('/api/LoanStatus/getAllLoanStatus2/1')
                    .then(response => (this.loanRegisterStatus = response.data))
             axios
                 .get('/api/ChartofAccount/getChartofAccount4/4')
                .then(response => (this.banktList = response.data))


        },

        methods: {
            isApprovalSelected() {
                if (this.postBody.status == 8) {
                    this.noapproval = true
                   
                }
            },
            checkForm: function (e) {
                console.log(this.postBody.status, this.postBody.id);
                if (this.postBody.status && this.postBody.id > 0) {
                    e.preventDefault();
                    this.canProcess = false;
                    this.postPost();
                }
                else {

                    this.errors = [];
                    this.errors.push('Code is Required');
                }
            },
            postPost() {

                axios.put(`/api/LoanRegister/ApproveLoanRegister`, this.postBody)
                    .then(response => {
                        this.responseMessage = response.data.responseDescription; this.canProcess = true;
                        if (response.data.responseCode == '200') {
                            this.submitorUpdate = 'Submit'
                            this.postBody.ChequeNo = ''; this.postBody.ChequeNo = '';
                            this.postBody.Description = ''; this.postBody.Description = '';
                            this.postBody.Tenure = ''; this.postBody.Tenure = '';
                            this.postBody.interest = ''; this.postBody.interest = '';
                            this.postBody.liabilityacct = ''; this.postBody.liabilityacct = '';
                            this.postBody.incomeacct = ''; this.postBody.incomeacct = '';
                            this.postBody.interestacct = ''; this.postBody.interestacct = '';
                            this.postBody.FundTypeID = ''; this.postBody.FundTypeID = '';
                        }
                    })
                    .catch(e => {
                        this.errors.push(e)
                    });

            }
        }
    };
</script>