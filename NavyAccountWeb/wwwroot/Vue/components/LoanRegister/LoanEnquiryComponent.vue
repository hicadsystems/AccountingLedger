<template>
    	<!-- WRAPPER -->
    <div>
    
        <div v-if="errors" class="alert alert-danger alert-dismissible" role="alert">
            <div class="alert-message">
                {{ [errors] }}
            </div>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">�</span>
            </button>
        </div>
        <div v-if="responseMessage" class="alert alert-primary alert-dismissible" role="alert"> <div class="alert-message"> {{ responseMessage }} </div> </div>
        <div class="card-body">

            <div class="row">
                <div class="col-12 col-xl-4">
                    <div class="form-group">
                        <label class="form-label">Service Number</label>
                        <vuejsAutocomplete source="/api/PersonAPI/getAllPersonsByServiceNoLimited/"
                                           input-class="form-control"
                                           @selected="setValuePersonID"
                                           v-model="postBody.PersonID">
                        </vuejsAutocomplete>

                    </div>
                </div>

                <div class="col-xl-3">
                    <div class="form-group">
                        <label class="form-label">Loan Type</label>

                        <select class="form-control" v-model="postBody.LoanTypeID" name="loanTypeID" required>
                            <option v-for="loantype in loantypeList" v-bind:value="loantype.id" v-bind:key="loantype.id"> {{ loantype.description }} </option>
                        </select>

                    </div>
                </div>

                <div class="row" style="position:relative;top:30px;">
                    <div class="col-4">
                        <div class="col-6 " v-if="postBody.PersonID != null && postBody.LoanTypeID != null">
                            <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group"><a class="btn btn-submit btn-primary" :href="'/LoanRepayment/CalaculateLoanRepayment?id='+ this.postBody.PersonID +'&loantypeid='+ this.postBody.LoanTypeID" target="_blank" type="button">{{submitorUpdate2}}</a></div>
                        </div>
                    </div>
                </div>

            </div>


            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <th>Application Number</th>

                        <th>Loan Name</th>
                        <th>Amount</th>

                        <th>Status</th>
                        <th>Status and Date</th>
                        <th>Cheque</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="personLoan in personApplications">
                        <td>{{ personLoan.applicationNo }}</td>

                        <td>{{ personLoan.loanTypeDesc }}</td>
                        <td>{{ personLoan.amount }}</td>
                        <td>{{ personLoan.status }}</td>
                        <td>{{ personLoan.statusAndDate }}</td>
                        <td>{{ personLoan.chequeNo }}</td>

                        <td><button type="button" class="btn btn-submit btn-primary" @click="viewDetails(personLoan)">View Details</button></td>
                    </tr>
                </tbody>

            </table>

        </div>


        <view-account-history-component v-if="refno" :refno="refnoVal" :accountcode="loanAccountVal" :svcno="svcno"></view-account-history-component>

    </div>
							
	<!-- END WRAPPER -->
</template>

<script>
    import vuejsAutocomplete from 'vuejs-auto-complete'
   
export default {
     components: {
         
         vuejsAutocomplete
        },
    props:['fundtypeid'],
    data() { 
        return {
        responseMessage:'',
            errors: null,
            searchData: '',
            submitorUpdate: 'Submit',
            submitorUpdate2:'Print Loan Repayment',
            personApplications: null,
            refno: false,
            loantypeList:null,
            refnoVal: '',
        loanAccountVal : '',
        canProcess : true,
        postBody: {
                PersonID:0,
                FundTypeID : null,
                
                Amount: 0,
                Status : '',
                VoucherNo : '',
                createdby :'',
                remarks: '',
                svcno:'',
                
                datecreated : null,
                Tenure : '',
                LoanTypeID:null

        }
        };
        },
        mounted() {
            this.$store.state.objectToUpdate = null,
         axios
            .get('/api/LoanType/getAllLoanDesc')
            .then(response => (this.loantypeList = response.data))
           
      
     },
     
    watch:{
        '$store.state.objectToUpdate':function (newVal, oldval) { 

               
        }
    },
        methods: {
         

        setValuePersonID(result) {
             axios
            .get(`/api/LoanRegister/getLoanListPerPerson/${result.value}/${this.fundtypeid}`)
            .then(response => (this.personApplications = response.data))

            },

            viewDetails(result) {
                this.refno = true
                
                this.refnoVal = result.applicationNo
                this.loanAccountVal = result.loanAccount
                this.svcno = result.svcno
                console.log(this.refnoVal)

         }
        }
};
</script>