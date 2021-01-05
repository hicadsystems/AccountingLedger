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
                <div class="col-12 col-xl-6" v-if="wanttoupdate">
                    <div class="form-group">
                        <label class="form-label">Service Number</label>
                        <vuejsAutocomplete source="/api/PersonAPI/getAllPersonsByServiceNoLimited/"
                                           input-class="form-control"
                                           @selected="setValuePersonID"
                                           v-model="postBody.PersonID">
                        </vuejsAutocomplete>

                    </div>
                </div>

                <div class="col-12 col-xl-6" v-if="!wanttoupdate">
                    <div class="form-group">
                        <label class="form-label">Service Number</label>
                        <input class="form-control" name="svcnno" v-model="fullName" placeholder="" readonly />
                    </div>
                </div>
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Loan Type</label>

                        <select class="form-control" v-model="postBody.LoanTypeID" name="loanTypeID" required>
                            <option v-for="loantype in loantypeList" v-bind:value="loantype.id" v-bind:key="loantype.id"> {{ loantype.description }} </option>
                        </select>

                    </div>
                </div>

                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Tenure</label>
                        <input class="form-control" name="Tenure" v-model="postBody.Tenure" placeholder="" />
                    </div>
                </div>
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Amount</label>
                        <input class="form-control" name="amount" v-model="postBody.Amount" placeholder="" />
                    </div>
                </div>
                <div class="col-12 col-xl-6">
                                    <label class="form-label">Status </label>

                                    <select class="form-control" v-model="postBody.status" name="status" @change="isApprovalSelected" required>
                                        <option v-for="st in loanStatusList" v-bind:value="st.id" v-bind:key="st.id"> {{ st.description }} </option>
                                    </select>

                                </div>
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Batch No</label>
                        <input class="form-control" name="batchNo" v-model="postBody.batchNo" placeholder="" />
                    </div>
                </div>
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">No Of Month Paid</label>
                        <input class="form-control" name="loancount" v-model="postBody.loancount" placeholder="" />
                    </div>
                </div>
                <div class="col-12 col-xl-6">
                    <div class="form-group">
                        <label class="form-label">Remarks</label>
                        <textarea class="form-control" name="chequeno" v-model="postBody.remarks" placeholder="" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-4">
                        <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="button">{{submitorUpdate}}</button></div>
                    </div>

                    <div class="col-6 " v-if="postBody.PersonID != null && postBody.LoanTypeID != null">
                        <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group"><a class="btn btn-submit btn-primary" :href="'/LoanSchedule/CalculateScheduleLoan?id='+ this.postBody.PersonID +'&loantypeid='+ this.postBody.LoanTypeID" target="_blank" type="button">{{submitorUpdate2}}</a></div>
                    </div>
                </div>




            </div>

        </div>

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
            wanttoupdate: true,
            fullName: '',

            searchData: '',
            loantypeList:null,
            submitorUpdate: 'Submit',
            submitorUpdate2:'Print Loan Schedule for Loan Repayment',
            new_actcode: '',
            loanStatusList: null,
            autoselectenabled: false,
        noapproval:false,
        canProcess : true,
        postBody: {
                PersonID:0,
                FundTypeID : null,
                
                Amount: 0,
                Status : '',
                VoucherNo : '',
                createdby :'',
                remarks : '',
                loancount: 0,
                batchNo:'',  
                datecreated : null,
                Tenure : '',
                LoanTypeID:null

        },
        mainaccountcodes:null,
        subtype: [
            { value: '0', text: 'GL Account' },
            { value: '2', text: 'Customers' },
            { value: '3', text: 'Brokers' },
            { value: '4', text: 'Bank' },
            { value: '5', text: 'Insurance Coys' },
            { value: '6', text: 'Mgt Expenses' },
            { value: '7', text: 'Tech Expenses' },
            { value: '8', text: 'Fixed Assets' },
            { value: '9', text: 'Income' }
    ]
        };
        },
        mounted() {
            this.$store.state.objectToUpdate = null,
             this.wanttoupdate = true,
        axios
            .get('/api/LoanType/getAllLoanDesc')
            .then(response => (this.loantypeList = response.data))
   

axios
            .get('/api/LoanStatus/getAllLoanStatus')
                .then(response => (this.loanStatusList = response.data))


      
     },
     
    watch:{
        '$store.state.objectToUpdate':function (newVal, oldval) { 

            this.fullName = this.$store.state.objectToUpdate.firstName + " "+ this.$store.state.objectToUpdate.lastName+ " "+ this.$store.state.objectToUpdate.middleName+" / "+this.$store.state.objectToUpdate.svcno 
         this.postBody.LoanTypeID = this.$store.state.objectToUpdate.loanTypeID,
         this.postBody.Tenure = this.$store.state.objectToUpdate.tenure,
         this.postBody.status = this.$store.state.objectToUpdate.status,
         this.postBody.Amount = this.$store.state.objectToUpdate.amount,
         this.postBody.remarks = this.$store.state.objectToUpdate.remarks,
             this.postBody.id = this.$store.state.objectToUpdate.id,
             this.postBody.loancount = this.$store.state.objectToUpdate.loancount,
                     this.postBody.batchNo = this.$store.state.objectToUpdate.batchNo,
         this.postBody.PersonID = this.$store.state.objectToUpdate.personID

            this.wanttoupdate = false;
                this.submitorUpdate = 'Update';this.autoselectenabled = true;
               
        }
    },
        methods: {
            loadData: function (e) {
                axios.get(`/api/LoanSchedule/CalculateScheduleLoan/${this.postBody.PersonID}/${this.postBody.LoanTypeID}`)
                    .then(response => {
                        this.responseMessage = response.data.responseDescription;
                        this.canProcess = true;
                        if (response.data.responseCode == '200') {
                            this.postBody.PersonID = ''; this.postBody.LoanTypeID = '';
                            //this.postBody.balSheetCode = '';this.postBody.subtype = '';
                            // this.wanttoupdate = true;
                        }

                    })
                  .catch(e => {
                            this.errors.push(e)
                  });
            },
        checkForm: function (e) {

             if (this.postBody.Tenure && this.postBody.LoanTypeID && this.postBody.Amount) {
             e.preventDefault();
             this.postBody.FundTypeID = this.fundtypeid;
              this.canProcess = false;
              this.postPost();
          }
          else{

          this.errors = [];
            this.errors.push('Supply the Compulsory filde(s)');
          }
        },
        pad(n,width,z){
            z = z || '0';
            n = n + '';
            return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
            },
        getLastUsedChartofAccount() {
            axios.get(`/api/ChartofAccount/getLastChartofAccount/${ this.postBody.mainAccountCode }`)
                        .then(response => {
                            let getSingleMainAct = this.mainaccountcodes.filter(x=>x.maincode == this.postBody.mainAccountCode);
                            
                            if (response.data.responseCode == '200' && response.data.data != 0) {

                                this.new_actcode = 0;
                                this.new_actcode = parseInt(response.data.data.acctcode.split('-')[1]) + 1;
                                this.new_actcode = this.postBody.mainAccountCode + "-"+this.pad(this.new_actcode,5);
                            }
                            else {

                                this.new_actcode = this.postBody.mainAccountCode + "-"+this.pad(1,5);
                            }
                                this.postBody.subtype = getSingleMainAct[0].subtype;
                                this.autoselectenabled = true;
                                this.postBody.balSheetCode = getSingleMainAct[0].npf_balsheet_bl_code;
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
            },

            setValuePersonID(result) {
                this.PersonID = result.Id
            },
        postPost() {

            
            if (this.submitorUpdate == 'Submit') {
                    this.postBody.acctcode = this.new_actcode;
                    axios.post(`/api/LoanRegister/createLoanRegister`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            if (response.data.responseCode == '200') {
                                this.postBody.PersonID = ''; this.postBody.Status = '';
                                 //this.postBody.balSheetCode = '';this.postBody.subtype = '';
                                this.wanttoupdate = true;
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
            }
          
            if (this.submitorUpdate == 'Update') {
                    axios.put(`/api/LoanRegister/updateLoanAppRegister`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;
                            this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                 this.submitorUpdate = 'Submit'
                                 this.postBody.mainAccountCode = '';this.postBody.description = '';
                                this.postBody.balSheetCode = ''; this.postBody.subtype = '';
                                this.wanttoupdate = true;
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            }
        }
};
</script>