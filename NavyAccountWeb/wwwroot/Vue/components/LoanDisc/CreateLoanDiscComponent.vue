<template>
    <!-- WRAPPER -->
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm" action="/LoanDisc/Loanrepayment" method="post">
            <div class="card-body">
                <div class="row">
               
                        <div class="col-12 col-xl-6">
                            <div class="form-group">
                                <label class="form-label">Service Number</label>
                                <vuejsAutocomplete source="/api/PersonAPI/getAllPersonsByServiceNoLimited/"
                                                   input-class="form-control"
                                                   @selected="setValuePersonID"
                                                   v-model="postBody.PersonID">
                                </vuejsAutocomplete>

                            </div>
                        </div>
                 
                    <div class="col-12 col-xl-6">
                        <div class="form-group">
                            <label class="form-label">Loan Type</label>

                            <select class="form-control" v-model="postBody.LoanTypeID" name="loanTypeID" required>
                                <option v-for="loantype in LoanacList" v-bind:value="loantype.id" v-bind:key="loantype.id"> {{ loantype.description }} </option>
                            </select>
                            
                        </div>
                    </div>
                    <div class="col-12 col-xl-6">
                        <div class="form-group">
                            <label class="form-label">Principal</label>
                            <input class="form-control" name="principal" v-model="postBody.principal" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-6">
                        <div class="form-group">
                            <label class="form-label">Interest</label>

                            <input class="form-control" name="interest" v-model="postBody.interest" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-6">
                        <div class="form-group">
                            <label class="form-label">Loan Amount</label>
                            <input class="form-control" name="loanpay" v-model="postBody.loanpay" placeholder="" />
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
     import vuejsAutocomplete from 'vuejs-auto-complete'
    export default {
         components: {
         
         vuejsAutocomplete
        },

        data() {
            return {
                errors: null,
                responseMessage: '',
                submitorUpdate: 'Submit',
                canProcess: true,
                LoanacList: null,
                postBody: {
                     PersonID:0,
                    Id:0,
                    loanact: '',
                    principal: '',
                    loanpay: '',
                    interest: '',
                    SVC_NO:'',
                    LoanTypeID:''
                   

                }

            }
        },
        mounted() {
           
            //axios
            //    .get('/api/ChartofAccount/getChartofAccount2/2')
            //    .then(response => (this.LoanacList = response.data))
          axios
            .get('/api/LoanType/getAllLoanDesc')
            .then(response => (this.LoanacList = response.data))
   

        },
        watch: {
            '$store.state.objectToUpdate': function (newVal, oldVal) {
                    this.postBody.loanact = this.$store.state.objectToUpdate.loanact,
                    this.postBody.principal = this.$store.state.objectToUpdate.principal,
                    this.postBody.interest = this.$store.state.objectToUpdate.interest,
                    this.postBody.loanpay = this.$store.state.objectToUpdate.loanpay
                   
                this.submitorUpdate = 'Update';

            }
        },
        methods: {
            checkForm: function (e) {

                if (this.postBody.PersonID) {
                    e.preventDefault();
                    this.canProcess = false;
                    this.postPost();
                }
                else {

                    this.errors = [];
                    this.errors.push('Code is Required');
                }
            },
             setValuePersonID(result) {
                this.PersonID = result.Id
            },
            postPost() {
                if (this.submitorUpdate == 'Submit') {
                    axios.post(`/api/Loandiscs/createLoandisc`, this.postBody)
                        .then(response => {
                            this.responseMessage = response.data.responseDescription; this.canProcess = true;
                            if (response.data.responseCode == '200') {
                                this.postBody.loanact = '';
                                this.postBody.principal = '';
                                this.postBody.loanpay = '';
                                this.postBody.interest = '';
                                this.postBody.LoanTypeID = '';
                                this.postBody.PersonID = '';

                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
                if (this.submitorUpdate == 'Update') {
                    axios.put(`/api/LoanType/updateLoanType`, this.postBody)
                        .then(response => {
                            this.responseMessage = response.data.responseDescription; this.canProcess = true;
                            if (response.data.responseCode == '200') {
                                this.submitorUpdate = 'Submit'
                                this.postBody.loanact = '';
                                this.postBody.principal = '';
                                this.postBody.loanpay = '';
                                this.postBody.interest = '';
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            }
        },
        computed: {
            setter() {
                let objecttoedit = this.$store.state.objectToUpdate;
                if (objecttoedit.Id) {
                    this.postBody.loanact = objecttoedit.loanact;
                    this.postBody.principal = objecttoedit.principal;
                    this.postBody.loanpay = objecttoedit.loanpay;
                    this.postBody.interest = objecttoedit.interest;
                   

                }
            }
        }
    };
</script>