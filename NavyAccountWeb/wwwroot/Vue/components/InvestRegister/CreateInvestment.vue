<template>
    <!-- WRAPPER -->
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm" action="/PfInvest/createInvestRegister" method="post">
            <div class="card-body">
                <input class="form-control" name="Id" v-model="postBody.ID" hidden />

                <div class="row">
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Date</label>
                            <vuejsDatepicker input-class="form-control" v-model="postBody.date" name="date"></vuejsDatepicker>
                        </div>
                    </div>

                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Deposit Bank</label>
                            <select class="form-control" v-model="postBody.IssuanceBankId" name="IssuanceBankId">
                                <option v-for="bk in bankList" v-bind:value="bk.id" v-bind:key="bk.id">{{ bk.bankname }}</option>
                            </select>
                        </div>
                    </div>


                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Voucher</label>
                            <input class="form-control" name="voucher" v-model="postBody.voucher" placeholder="" />
                        </div>
                    </div>

                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Remark</label>
                            <textarea class="form-control" name="description" v-model="postBody.description" placeholder="" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Amount</label>
                            <input class="form-control" name="amount" v-model="postBody.amount" placeholder="" />
                        </div>
                    </div>

                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">DueDate</label>
                            <vuejsDatepicker input-class="form-control" name="duedate" v-model="postBody.duedate" placeholder=""></vuejsDatepicker>
                        </div>
                    </div>




                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label" v-if="readonly">Tenure</label>
                            <input class="form-control" v-if="readonly" name="tenure" v-model="postBody.tenure" placeholder="" />
                        </div>
                    </div>

                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label v-if="readonly">Interest Rate</label>
                            <input class="form-control" name="interest" v-model="postBody.interest" v-if="readonly" placeholder="" />
                        </div>

                    </div>
                </div>
                <div class="row">

                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label" v-if="readonly">MaturingDate</label>
                            <vuejsDatepicker input-class="form-control" name="maturingdate" v-model="postBody.maturingdate" placeholder=""></vuejsDatepicker>
                        </div>
                    </div>


                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label v-if="readonly" class="form-label">cheque No</label>
                            <input v-if="readonly" class="form-control" name="chequeno" v-model="postBody.chequeno" placeholder="" />
                        </div>

                    </div>


                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label v-if="readonly" class="form-label">Matured Amount</label>
                            <input v-if="readonly" class="form-control" name="maturedamt" v-model="postBody.maturedamt" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label v-if="readonly" class="form-label">Receiving Bank</label>
                            <select v-if="readonly" class="form-control" v-model="postBody.receivingbankid" name="receivingbankid">
                                <option v-for="bk in bankList" v-bind:value="bk.id" v-bind:key="bk.id">{{ bk.bankname }}</option>
                            </select>
                        </div>
                    </div>
                </div>

                <div class="row">
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
    
     import vuejsDatepicker from 'vuejs-datepicker';
    export default {
        
        components: {
            vuejsDatepicker
        },
    data() {
        return {
            errors: null,
            responseMessage: '',
            submitorUpdate: 'Submit',
            canProcess: true,
            userList: null,
            bankList: null,
            InvestTypeList: null,
            readonly:true,
            postBody: {
              
                IssuanceBankId: '',
                receivingbankid: '',
                interest: '',
                maturingdate: '',
                tenure:'',
                voucher: '',
                description: '',
                amount: 0,
                date: '',
                duedate: '',
                investmenttype:'',
                chequeno: '',
                maturedamt: 0
                
            },
            invest: [
                    { value: 'Money Market', text: 'Money Market' },
                    { value: 'Capital Market', text: 'Capital Market' }
                ]
        }
        },
      mounted () {
        axios
            .get('/api/Bank/getAllBanks')
              .then(response => (this.bankList = response.data)),
           axios
            .get('/api/PfInvest/LoadAllPersonnel')
                  .then(response => (this.userList = response.data))
             
     },

    watch:{
        '$store.state.objectToUpdate': function (FundCode, Rate, Period, Intrest) {
                
                this.postBody.IssuanceBankId = this.$store.state.objectToUpdate.issuanceBankId,
                this.postBody.voucher = this.$store.state.objectToUpdate.voucher,
                this.postBody.description = this.$store.state.objectToUpdate.description,
                this.postBody.amount = this.$store.state.objectToUpdate.amount,
                this.postBody.date = this.$store.state.objectToUpdate.date,
                this.postBody.duedate = this.$store.state.objectToUpdate.dueDate,
                this.postBody.InvestmentType = this.$store.state.objectToUpdate.investmentType,
                this.postBody.chequeno = this.$store.state.objectToUpdate.chequeno,
                this.postBody.maturedamt = this.$store.state.objectToUpdate.maturedamt,
                this.postBody.tenure = this.$store.state.objectToUpdate.tenure,
                this.postBody.interest = this.$store.state.objectToUpdate.interest,
                this.postBody.maturingdate = this.$store.state.objectToUpdate.maturingdate,
                this.postBody.receivingbankid = this.$store.state.objectToUpdate.receivingbankid,
                this.postBody.Id = this.$store.state.objectToUpdate.id
                this.submitorUpdate = 'Update';

        }
    },
        methods: {
            onChange(event) {
                if (event.target.value == 'Money Market') {
                    this.readonly = false;
                } else {
                    this.readonly = true;
                }
            },
          checkForm: function (e) {
            e.preventDefault();

           
         if (this.postBody.IssuanceBankId) {
              e.preventDefault();
              this.canProcess = false;
              this.postPost();
          }
          else{

            this.errors = [];
            this.errors.push('issuanceBank is Required');
          }
        },
        postPost() {

                if(this.submitorUpdate == 'Submit'){
                    axios.post(`/api/PfInvest/createInvestRegister`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if (response.data.responseCode == '200') {

                                this.$store.state.objectToUpdate = "create";
                                this.postBody.issuanceBankId = '';
                                this.postBody.receivingbankid = '';
                                this.postBody.voucher = '';
                                this.postBody.description= '';
                                this.postBody.amount= 0;
                                this.postBody.date='';
                                this.postBody.duedate='';
                                this.postBody.investmenttype= '';
                                this.postBody.closecode = '';
                                this.postBody.chequeno = '';
                                this.postBody.maturedamt = 0;
                                this.maturingdate = '';
                                this.interest = '';
                                this.tenure = '';
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            if (this.submitorUpdate == 'Update') {

                    axios.put(`/api/PfInvest/updateInvestRegister`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if (response.data.responseCode == '200') {

                               this.submitorUpdate = 'Submit';
                               this.$store.state.objectToUpdate = "Update";
                                this.postBody.issuanceBankId = '';
                                 this.postBody.receivingbankid = '';
                                this.postBody.voucher = '';
                                this.postBody.description= '';
                                this.postBody.amount= 0;
                                this.postBody.date='';
                                this.postBody.duedate='';
                                this.postBody.investmenttype= '';
                                this.postBody.closecode = '';
                                this.postBody.chequeno = '';
                                this.postBody.maturedamt = 0;
                                this.maturingdate = '';
                                this.interest = '';
                                this.tenure = '';
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

                if (objecttoedit!=null) {
                    
                    this.postBody.issuanceBankId = objecttoedit.issuanceBankId;
                    this.postBody.receivingbankid = objecttoedit.receivingbankid;
                    this.postBody.voucher = objecttoedit.voucher;
                    this.postBody.description = objecttoedit.description;
                    this.postBody.amount = objecttoedit.amount;
                    this.postBody.date = objecttoedit.date;
                    this.postBody.duedate = objecttoedit.duedate;
                    this.postBody.investmenttype = objecttoedit.investmenttype;
                    this.postBody.closecode = objecttoedit.closecode;
                    this.postBody.chequeno = objecttoedit.chequeno;
                    this.postBody.maturedamt = objecttoedit.maturedamt;
                    this.postBody.tenure = objecttoedit.tenure;
                    this.postBody.interest = objecttoedit.interest;
                }
            }
        }
};
</script>