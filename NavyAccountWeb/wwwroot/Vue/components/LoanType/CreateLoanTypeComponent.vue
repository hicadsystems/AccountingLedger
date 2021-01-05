<template>
    	<!-- WRAPPER -->
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm" action="/LoanType/CreateLoanType" method="post">
            <div class="card-body">
                <div class="row">
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Code</label>
                            <input type="text" name="Code" class="form-control" v-model="postBody.Code" required :readonly="submitorUpdate == 'Update'" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Description</label>
                            <input class="form-control" name="Description" v-model="postBody.Description" placeholder="Description" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Fund Type</label>

                            <select class="form-control" v-model="postBody.FundTypeID" name="fundtype" required>
                                <option v-for="fundtype in FundTypeList" v-bind:value="fundtype.id" v-bind:key="fundtype.id"> {{ fundtype.description }} </option>
                            </select>

                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Tenure</label>
                            <input class="form-control" name="Tenure" v-model="postBody.Tenure" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Interest</label>
                            <input class="form-control" name="Interest" v-model="postBody.Interest" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Income Account</label>
                            <select class="form-control" v-model="postBody.incomeacct" name="incomeacct" required>
                                <option v-for="liability in LiabilityList" v-bind:value="liability.acctcode" v-bind:key="liability.acctcode"> {{ liability.description }} </option>
                            </select>
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Interest Account</label>

                            <select class="form-control" v-model="postBody.interestacct" name="interestacct" required>
                                <option v-for="interest in InterestList" v-bind:value="interest.acctcode" v-bind:key="interest.acctcode"> {{ interest.description }} </option>
                            </select>
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">Loan Account</label>

                            <select class="form-control" v-model="postBody.loanacct" name="loanacct" required>
                                <option v-for="Loanac in LoanacList" v-bind:value="Loanac.acctcode" v-bind:key="Loanac.acctcode"> {{ Loanac.description }} </option>
                            </select>
                        </div>
                    </div>
                    <div class="col-12 col-xl-4">
                        <div class="form-group">
                            <label class="form-label">NHQ Trustee Account</label>

                            <select class="form-control" v-model="postBody.trusteeacct" name="trusteeacct" required>
                                <option v-for="Trustee in TrusteeList" v-bind:value="Trustee.acctcode" v-bind:key="Trustee.acctcode"> {{ Trustee.description }} </option>
                            </select>
                        </div>
                    </div>
                    
                    <div class="col-12 ">
                        <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group"><button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button></div>
                    </div>
                </div>
                </div>
        </form>
    </div>
							
	<!-- END WRAPPER -->
</template>

<script>
export default {
    
    data() {
        return {
            errors: null,
            responseMessage:'',
            submitorUpdate : 'Submit',
            canProcess : true,
            FundTypeList: null,
            InterestList: null,
            LiabilityList:null,
            LoanacList: null,
            TrusteeList: null,
            postBody: {
                Code: '',
                Description:'',
                Tenure: '',
                Interest:'',
                liabilityacct:'',
                incomeacct:'',
                interestacct: '',
                loanacct: '',
                trusteeacct:'',
                FundTypeID:''

            }
    
        }
        },
     mounted () {
        axios
            .get('/api/FundTypeCode/getAllFundTypeCodes')
             .then(response => (this.FundTypeList = response.data))
         axios
            .get('/api/ChartofAccount/getChartofAccount1/1')
             .then(response => (this.InterestList = response.data))
          
         axios
            .get('/api/ChartofAccount/getChartofAccount4/4')
             .then(response => (this.LiabilityList = response.data))
            axios
            .get('/api/ChartofAccount/getChartofAccount2/2')
                .then(response => (this.TrusteeList = response.data))

          axios
         .get('/api/ChartofAccount/getChartofAccount3/3')
             .then(response => (this.LoanacList = response.data))
       
     },
    watch:{
         '$store.state.objectToUpdate':function (newVal, oldVal) {
         this.postBody.Code = this.$store.state.objectToUpdate.code,
         this.postBody.Description = this.$store.state.objectToUpdate.description,
         this.postBody.Tenure = this.$store.state.objectToUpdate.tenure,
         this.postBody.Interest = this.$store.state.objectToUpdate.interest,
         this.postBody.incomeacct = this.$store.state.objectToUpdate.incomeacct,
         this.postBody.interestacct = this.$store.state.objectToUpdate.interestacct,
         this.postBody.loanacct = this.$store.state.objectToUpdate.loanacct,
         this.postBody.trusteeacct = this.$store.state.objectToUpdate.trusteeacct,
         this.postBody.FundTypeID = this.$store.state.objectToUpdate.fundTypeID

         this.submitorUpdate = 'Update';
               
        }
    },
    methods: {
        checkForm: function (e) {
            
         if (this.postBody.Code) {
              e.preventDefault();
              this.canProcess = false;
              this.postPost();
          }
          else{

          this.errors = [];
            this.errors.push('Code is Required');
          }
        },
        postPost() {
                if(this.submitorUpdate == 'Submit'){
                    axios.post(`/api/LoanType/createLoanType`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if (response.data.responseCode == '200') {
                                this.postBody.Code = ''; this.postBody.Code = '';
                                this.postBody.Description = ''; this.postBody.Description = '';
                                this.postBody.Tenure = '';this.postBody.Tenure = '';
                                this.postBody.Interest = '';this.postBody.Interest = '';
                                this.postBody.incomeacct = '';this.postBody.incomeacct = '';
                                this.postBody.interestacct = ''; this.postBody.interestacct = '';
                                this.postBody.loanacct = ''; this.postBody.loanacct = '';
                                this.postBody.trusteeacct = ''; this.postBody.trusteeacct = '';
                                this.postBody.FundTypeID = ''; this.postBody.FundTypeID = '';
                                  this.$store.state.objectToUpdate = "create";
                                
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
                if(this.submitorUpdate == 'Update'){
                    axios.put(`/api/LoanType/updateLoanType`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                this.submitorUpdate = 'Submit'
                                this.postBody.Code = '';this.postBody.Code = '';
                                this.postBody.Description = '';this.postBody.Description = '';
                                this.postBody.Tenure = ''; this.postBody.Tenure = '';
                                this.postBody.interest = ''; this.postBody.interest = '';
                                this.postBody.incomeacct = '';this.postBody.incomeacct = '';
                                this.postBody.interestacct = ''; this.postBody.interestacct = '';
                                this.postBody.loanacct = ''; this.postBody.loanacct = '';
                                this.postBody.trusteeacct = ''; this.postBody.trusteeacct = '';
                                this.postBody.FundTypeID = ''; this.postBody.FundTypeID = '';
                                  this.$store.state.objectToUpdate = "update";
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            }
        },
        computed: {
            setter(){
                let objecttoedit = this.$store.state.objectToUpdate;
                if(objecttoedit.Code){
                    this.postBody.Code = objecttoedit.Code;
                    this.postBody.Description = objecttoedit.Description;
                    this.postBody.Tenure = objecttoedit.Tenure;
                    this.postBody.Interest = objecttoedit.Interest;
                    this.postBody.incomeacct = objecttoedit.incomeacct;
                    this.postBody.interestacct = objecttoedit.interestacct;
                    this.postBody.loanacct = objecttoedit.loanacct;
                    this.postBody.trusteeacct = objecttoedit.trusteeacct;
                    this.postBody.FundTypeID = objecttoedit.FundTypeID;
                  
                }
            }
        }
};
</script>